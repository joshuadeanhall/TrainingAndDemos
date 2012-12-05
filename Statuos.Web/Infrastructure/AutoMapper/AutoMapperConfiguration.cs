using AutoMapper;
using Castle.Windsor;
using Statuos.Web.Infrastructure.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure(IWindsorContainer _container)
        {
            var profiles = _container.ResolveAll<Profile>();            
            foreach (var profile in profiles)
            {
                Mapper.AddProfile(profile);
            }
            
        }
    }
}