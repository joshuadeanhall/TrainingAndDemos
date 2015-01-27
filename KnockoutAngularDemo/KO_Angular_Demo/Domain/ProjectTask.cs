using System.Security.AccessControl;
using KO_Angular_Demo.Models;
using MongoDB.Bson;

namespace KO_Angular_Demo.Domain
{
    public class ProjectTask
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Effort { get; set; }
        public ApplicationUser AssignedToUser { get; set; }
    }
}