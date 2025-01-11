using AutoMapper;
using Entities.ModelDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
           CreateMap<BookDto,Book>().ReverseMap();
           CreateMap<AuthorDto,Author>().ReverseMap();
           CreateMap<PublishingHouseDto,PublishingHouse>().ReverseMap();
        }
    }
}
