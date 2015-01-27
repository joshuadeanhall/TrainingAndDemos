using AutoMapper;
using Statuos.Domain;
using Statuos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.AutoMapper.Profiles
{
    public class TaskProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Task, TaskViewModel>()
                .Include<BasicTask, BasicTaskViewModel>()
                .Include<PhoneRequestTask, PhoneRequestTaskViewModel>();
            Mapper.CreateMap<PhoneRequestTask, PhoneRequestTaskViewModel>();
            Mapper.CreateMap<BasicTask, BasicTaskViewModel>();

            Mapper.CreateMap<TaskViewModel, Task>()            
                .Include<BasicTaskViewModel, BasicTask>()
                .Include<PhoneRequestTaskViewModel, PhoneRequestTask>();

            Mapper.CreateMap<PhoneRequestTaskViewModel, PhoneRequestTask>()
                .ForMember(dest => dest.Project, opt => opt.Ignore());
            Mapper.CreateMap<BasicTaskViewModel, BasicTask>()
                .ForMember(dest => dest.Project, opt => opt.Ignore());

            Mapper.CreateMap<Project, TaskViewModel.ProjectDetails>()
                .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.ProjectManager.UserName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));

            Mapper.CreateMap<User, TaskViewModel.UserDetails>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

            Mapper.CreateMap<Task, TaskUserViewModel>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<Charge, TaskViewModel.ChargeDetails>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

        }
    }
}