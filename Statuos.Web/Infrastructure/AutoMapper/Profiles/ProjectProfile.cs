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
    public class ProjectProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ProjectManager.UserName))  
                .ForMember(dest => dest.CustomerName,opt => opt.MapFrom(src => src.Customer.Name))
                .Include<BasicProject, BasicProjectViewModel>()
                .Include<MaxHoursProject, MaxHoursProjectViewModel>();
            Mapper.CreateMap<BasicProject, BasicProjectViewModel>();
            Mapper.CreateMap<MaxHoursProject, MaxHoursProjectViewModel>();

            Mapper.CreateMap<ProjectViewModel, Project>()
                .Include<BasicProjectViewModel, BasicProject>()
                .Include<MaxHoursProjectViewModel, MaxHoursProject>();
            Mapper.CreateMap<BasicProjectViewModel, BasicProject>();
            Mapper.CreateMap<MaxHoursProjectViewModel, MaxHoursProject>();
        }

    }
}