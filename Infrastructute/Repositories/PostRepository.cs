using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructute.Repositories
{
    public class PostRepository : IPostRepository

        //to jest glowna klasa przechowywania postow i od niej masz inne ruchy
    {
        // tutaj w _posts sa zapisane posty... i to dziedziczy po ipostrepository...
        //alle jak to sie dzieje ze idziemy do domeny....
        private static readonly ISet<Post> _posts = new HashSet<Post>() { 
        new Post (1, "Post1","zawartosc1"), 
        new Post (2, "Post2","zawartosc2"), //sprawdz co sie stanie jak id te same bd
        new Post (3, "Post3","zawartosc3") // bez sprawdzania powiem ze singleordefault znajdzie pierwszy i go zwroci
        
        };
        public IEnumerable<Post> GetAll()
        {
            return _posts;
        }

        public Post GetById(int id)
        {
            return _posts.SingleOrDefault(x => x.Id == id);
        }

        public Post Add(Post post)
        {
            post.Id = _posts.Count + 1; // no proste, bo wszedł nowy post i nadajesz mu poprawne id
            post.Created = DateTime.UtcNow ;
            _posts.Add(post);
            return post;
        }
        public void Update(Post post)
        {
            post.LastModified = DateTime.UtcNow; 
        }
        public void Delete(Post post)
        {
            _posts.Remove(post);
        }

       
      
    }
}
