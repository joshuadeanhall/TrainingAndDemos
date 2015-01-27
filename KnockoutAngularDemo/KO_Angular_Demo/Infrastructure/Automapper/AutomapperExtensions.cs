using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace KO_Angular_Demo.Infrastructure.Automapper
{
    public static class AutoMapperExtensions
    {
        public static List<TResult> MapTo<TResult>(this IEnumerable self)
        {
            return (List<TResult>)Mapper.Map(self, self.GetType(), typeof(List<TResult>));
        }
        public static TResult MapTo<TResult>(this object self)
        {
            return (TResult)Mapper.Map(self, self.GetType(), typeof(TResult));
        }
    }
}