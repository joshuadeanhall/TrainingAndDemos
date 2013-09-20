using AutoMapper;
using MBlog.Areas.Admin.Models;
using MBlog.Domain;

namespace MBlog.Infrastructure.Automapper.Profiles
{
    public class AdminUserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserViewModel, User>();
        }
    }
}