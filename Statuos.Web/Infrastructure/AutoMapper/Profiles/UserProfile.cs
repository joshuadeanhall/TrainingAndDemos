using AutoMapper;
using Statuos.Domain;
using Statuos.Web.Areas.Admin.Models;
using Statuos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserViewModel, User>();
        }
    }
}