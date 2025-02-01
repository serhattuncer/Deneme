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
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<PublishingHouseDto, PublishingHouse>().ReverseMap();
            CreateMap<UsersDto, Users>().ReverseMap();
            CreateMap<RolesDto, Roles>().ReverseMap();
            CreateMap<ClaimsDto, Claims>().ReverseMap();
            CreateMap<UserClaimsDto, UserClaims>().ReverseMap();
            CreateMap<RoleClaimsDto, RoleClaims>().ReverseMap();
            CreateMap<UserRolesDto, UserRoles>().ReverseMap();
        }
    }
}
