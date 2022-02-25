using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;
using Domain.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructute.Identity;
using Application.Services;

namespace Infrastructute.Data 
{
    public class BloggerContext : IdentityDbContext<ApplicationUser>  //  tu byl blad i nie chcialo zrobic migracji
    {
        private readonly UserResolverService _userService;
        /// dodatkowo klasa kontekstu blogger context 
        public BloggerContext(DbContextOptions <BloggerContext> options, UserResolverService userService) : base(options) // wstrzykujemy 
        {
            _userService = userService;
        }
        public DbSet<Post> Posts { get; set; }

        public async Task< int> SaveChangesAsync    () //opakowany typ int 
        {
            //wyszukujemy wszystkie encje ktore sa typy auditable entity - te od czasu na poczatk utworzone
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).LastModifiedBy = _userService.GetUser(); // dopiero po wstrzyknieciu zaleznosci mozna wywolac metode i zwrocic nazwe uzytkownika do odpowiednich miejsc w db
                // jak uzyskac dostep w tej klasie do informacji o uzytkowniku ?  poprzez stworzenie specjalnej klasy serwisu  ktora wstrzyknie tutaj kontekts http
                //user resolver service 

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = _userService.GetUser(); // dopiero po wstrzyknieciu zaleznosci mozna wywolac metode i zwrocic nazwe uzytkownika do odpowiednich miejsc w db

                }

            }

            return await base.SaveChangesAsync();
        }
    }
}
