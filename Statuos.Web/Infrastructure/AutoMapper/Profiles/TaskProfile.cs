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
                .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.Project.Title))
                .Include<BasicTask, BasicTaskViewModel>()
                .Include<PhoneRequestTask, PhoneRequestTaskViewModel>();
            Mapper.CreateMap<PhoneRequestTask, PhoneRequestTaskViewModel>();
            Mapper.CreateMap<BasicTask, BasicTaskViewModel>();

            Mapper.CreateMap<TaskViewModel, Task>()
                .Include<BasicTaskViewModel, BasicTask>()
                .Include<PhoneRequestTaskViewModel, PhoneRequestTask>();
            Mapper.CreateMap<PhoneRequestTaskViewModel, PhoneRequestTask>();
            Mapper.CreateMap<BasicTaskViewModel, BasicTask>();
                    
        }
    }
}