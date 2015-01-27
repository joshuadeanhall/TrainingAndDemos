using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.Domain
{
    public class Blog
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
