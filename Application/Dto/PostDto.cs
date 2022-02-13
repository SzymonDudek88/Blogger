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

        public DateTime CreationDate  { get; set; }

        public void Mapping(Profile profile) // Profile to klasa automappera
        {
            profile.CreateMap<Post, PostDto>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.Created)) //to odpowiada za date dodania posta
                ; 
        }

        // celowo nie tworzymy  created, modified, lastmodified itd bo nie chcemy ich zwracac do WEB API
    }
}
