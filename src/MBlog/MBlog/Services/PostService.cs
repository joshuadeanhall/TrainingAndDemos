using System;
using System.Collections.Generic;
using System.Linq;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ServiceStack.ServiceHost;

namespace MBlog.Services
{
    public class PostService : IService
    {
        private readonly MongoDatabase _database;

        public PostService(MongoDatabase database)
        {
            if (database == null) throw new ArgumentNullException("database");
            _database = database;
        }

        public IEnumerable<PostResponse> Get(Posts postRequest)
        {
            var posts = _database.GetCollection<Post>("posts").AsQueryable().OrderByDescending(p => p.PublishDate).ToList();
            var result = posts.MapTo<PostResponse>();
            return result;
        }

        public PostResponse Get(GetPost getPostRequest)
        {
            var collection = _database.GetCollection<Post>("posts");
            var post = collection.AsQueryable().SingleOrDefault(p => p.PostId == new ObjectId(getPostRequest.Id));
            var response = post.MapTo<PostResponse>();
            return response;
        }

        public AboutResponse Get(AboutMeRequest aboutRequest)
        {
            var collection = _database.GetCollection<Setting>("settings");
            var aboutMe = collection.AsQueryable().Single(a => a.Name == "About Me");
            return new AboutResponse {Content = aboutMe.Value};
        }
    }
}