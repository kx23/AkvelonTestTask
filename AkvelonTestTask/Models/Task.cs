using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace AkvelonTestTask.Models
{
    // Task class stores information about a task
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }

    }

    // Task statuses
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}
