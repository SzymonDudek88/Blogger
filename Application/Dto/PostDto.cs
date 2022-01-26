using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
   public  class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        // celowo nie tworzymy  created, modified, lastmodified itd bo nie chcemy ich zwracac do WEB API
    }
}
