using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using KO_Angular_Demo.Domain;
using KO_Angular_Demo.Models;

namespace KO_Angular_Demo.Infrastructure.Automapper.Profiles
{
    public class ProjectProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Project, ProjectViewModel>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(src => src.ProjectManager.UserName));
            Mapper.CreateMap<ProjectViewModel, Project>();
        }
    }
}