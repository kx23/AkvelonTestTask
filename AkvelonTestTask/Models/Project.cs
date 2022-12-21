using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;


namespace AkvelonTestTask.Models
{
    // Project class stores information about a project
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public ProjectStatus Status { get; set; }
        public int Priority { get; set; }

        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; }
    }

    // Project statuses
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
}
