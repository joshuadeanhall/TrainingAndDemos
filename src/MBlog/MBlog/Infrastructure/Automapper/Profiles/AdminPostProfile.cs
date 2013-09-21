using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MBlog.Areas.Admin.Models;
using MBlog.Domain;

namespace MBlog.Infrastructure.Automapper.Profiles
{
    public class AdminPostProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostViewModel, Post>();
        }
    }
}