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
                .ForMember(dest =>  dest.Charges, opt => opt.Ignore())
                .Include<BasicProject, BasicProjectViewModel>()
                .Include<MaxHoursProject, MaxHoursProjectViewModel>();
            Mapper.CreateMap<BasicProject, BasicProjectViewModel>()
                .ForMember(dest => dest.Charges, opt => opt.Ignore());

            Mapper.CreateMap<MaxHoursProject, MaxHoursProjectViewModel>()
                .ForMember(dest => dest.Charges, opt => opt.Ignore());

            Mapper.CreateMap<ProjectViewModel, Project>()
                .ForMember(dest => dest.Charges, opt => opt.Ignore())
                .Include<BasicProjectViewModel, BasicProject>()
                .Include<MaxHoursProjectViewModel, MaxHoursProject>();
            Mapper.CreateMap<BasicProjectViewModel, BasicProject>()
                .ForMember(dest => dest.Charges, opt => opt.Ignore());
            Mapper.CreateMap<MaxHoursProjectViewModel, MaxHoursProject>()
                .ForMember(dest => dest.Charges, opt => opt.Ignore());

            Mapper.CreateMap<Charge, ProjectViewModel.ProjectChargeDetails>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Hours, opt => opt.MapFrom(src => src.Hours))
                .ForMember(dest => dest.TaskTitle, opt => opt.MapFrom(src => src.Task.Title))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName));
        }

    }
}