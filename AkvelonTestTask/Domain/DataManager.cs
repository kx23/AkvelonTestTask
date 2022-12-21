using AkvelonTestTask.Domain.Repositories.Abstract;

namespace AkvelonTestTask.Domain
{
    // DataManager is a class that makes it easier to interact with and manage repositories.
    public class DataManager
    {
        public IProjectsRepository ProjectsRepository { get; set; }
        public ITasksRepository TasksRepository { get; set; }
        public DataManager(IProjectsRepository projectsRepository,
            ITasksRepository tasksRepository)
        {
            ProjectsRepository = projectsRepository;
            TasksRepository = tasksRepository;
        }
    }
}
