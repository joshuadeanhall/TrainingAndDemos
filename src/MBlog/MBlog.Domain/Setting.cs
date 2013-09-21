using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MBlog.Domain
{
    public class Setting
    {
        [BsonId]
        public ObjectId SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
