using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MBlog.Domain
{
    public class User
    {
        [BsonId]
        public ObjectId UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
