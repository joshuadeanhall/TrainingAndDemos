using System;
using System.Collections.Generic;
using KO_Angular_Demo.Models;
using MongoDB.Bson;

namespace KO_Angular_Demo.Domain
{
    public class Project
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public ApplicationUser ProjectManager { get; set; }
        public int Effort { get; set; }
        public float Cost { get; set; }
    }
}
