using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MBlog.Domain
{
    public class Post
    {
        [BsonId]
        public ObjectId PostId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
    }
}
