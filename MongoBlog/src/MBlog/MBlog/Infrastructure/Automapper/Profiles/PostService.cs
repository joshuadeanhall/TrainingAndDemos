using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MBlog.Domain;
using MBlog.Services;

namespace MBlog.Infrastructure.Automapper.Profiles
{
    public class PostService : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Post, PostResponse>();
            Mapper.CreateMap<PostResponse, Post>();
        }
    }
}