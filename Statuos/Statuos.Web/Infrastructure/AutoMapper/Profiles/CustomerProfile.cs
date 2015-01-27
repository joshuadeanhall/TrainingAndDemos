using AutoMapper;
using Statuos.Domain;
using Statuos.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.AutoMapper.Profiles
{
    public class CustomerProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Customer, CustomerViewModel>();
            Mapper.CreateMap<CustomerViewModel, Customer>();
        }
    }
}