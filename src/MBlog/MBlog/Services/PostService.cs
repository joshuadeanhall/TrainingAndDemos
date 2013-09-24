using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBlog.Domain;
using MongoDB.Driver;
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

        public IEnumerable<PostResponse> Get(PostRequest postRequest)
        {
            var posts = _database.GetCollection<Post>("posts").FindAll();
            var result =
                posts.Select(
                    post =>
                        new PostResponse {Content = post.Content, PublishedOn = post.PublishDate, Title = post.Title});
            return result;
        }
    }
}