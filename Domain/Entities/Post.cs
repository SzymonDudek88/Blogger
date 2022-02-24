using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    [Table("Posts")] // sluzy do konfigurowania nazwy tabeli w bazie danych 
  public class Post : AuditableEntity // tu dziedziczy po klasie wiec posiada takze wlasciwosci odnosnie kreacji
        // i czasu itd. - sprawa poboczna
    {
        [Key] // to ja to robilem z poziomu menagera db
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]      
        public string Title { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }  // id z bazy danych, chyba potrzebne do logowania 
        public Post()
        {

        }
        public Post(int id, string title, string content)
        {
            (Id, Title, Content) = (id, title, content);

        }


    }
}
