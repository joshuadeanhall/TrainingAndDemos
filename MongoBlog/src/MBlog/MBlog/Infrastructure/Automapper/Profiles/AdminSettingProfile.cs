using AutoMapper;
using MBlog.Areas.Admin.Models;
using MBlog.Domain;

namespace MBlog.Infrastructure.Automapper.Profiles
{
    public class AdminSettingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Setting, SettingViewModel>();
            Mapper.CreateMap<SettingViewModel, Setting>();
        }
    }
}