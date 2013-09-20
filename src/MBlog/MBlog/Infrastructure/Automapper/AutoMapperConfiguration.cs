using AutoMapper;
using MBlog.Infrastructure.Automapper.Profiles;

namespace MBlog.Infrastructure.Automapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
                Mapper.AddProfile(new AdminUserProfile());
        }
    }
}