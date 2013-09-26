using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
            var posts = _database.GetCollection<Post>("posts").FindAll();
            var result =
                posts.Select(
                    post =>
                        new PostResponse { PostId = post.PostId.ToString(), Content = post.Content, PublishedOn = post.PublishDate, Title = post.Title });
            return result;
        }

        public PostResponse Get(GetPost getPostRequest)
        {
            var collection = _database.GetCollection<Post>("posts");
            var post = collection.FindOne(Query.EQ("_id", new BsonObjectId(getPostRequest.Id)));

            var response = post.MapTo<PostResponse>();
            return response;
        }

        public AboutResponse Get(AboutMeRequest aboutRequest)
        {
            var collection = _database.GetCollection<Setting>("settings");
            var aboutMe = collection.FindOne(Query.EQ("Name", "About Me"));
            return new AboutResponse {Content = aboutMe.Value};
        }
    }
}