using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
   public static class AutoMapperConfig
    {
        // to cala konfiguracja mapowania
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostDto>();
            }
            ).CreateMapper();
    }
}
