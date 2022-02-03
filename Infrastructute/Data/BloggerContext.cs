﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;
using Domain.Common;
 

namespace Infrastructute.Data
{
    public class BloggerContext : DbContext
    {
        public BloggerContext(DbContextOptions options) : base(options)
        {
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

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.UtcNow;

                }

            }

            return await base.SaveChangesAsync();
        }
    }
}