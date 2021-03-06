﻿using AutoMapper;
using MBlog.Infrastructure.Automapper.Profiles;

namespace MBlog.Infrastructure.Automapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.AddProfile(new AdminUserProfile());
            Mapper.AddProfile(new AdminSettingProfile());
            Mapper.AddProfile(new PostProfile());
            Mapper.AddProfile(new PostService());
        }
    }
}