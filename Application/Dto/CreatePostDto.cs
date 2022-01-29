using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto
{
   public class CreatePostDto : IMap
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
         profile.CreateMap<CreatePostDto, Post>();
        }
    }
}
