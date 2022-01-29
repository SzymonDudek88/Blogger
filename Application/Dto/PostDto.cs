using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO
{
   public  class PostDto : IMap
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostDto>();
        }

        // celowo nie tworzymy  created, modified, lastmodified itd bo nie chcemy ich zwracac do WEB API
    }
}
