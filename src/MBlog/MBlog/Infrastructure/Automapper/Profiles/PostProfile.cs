using AutoMapper;
using MBlog.Domain;
using MBlog.Models;

namespace MBlog.Infrastructure.Automapper.Profiles
{
    public class PostProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostViewModel, Post>();
        }
    }
}