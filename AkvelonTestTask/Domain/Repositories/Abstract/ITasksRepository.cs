using AkvelonTestTask.Models;
using System.Linq;

namespace AkvelonTestTask.Domain.Repositories.Abstract
{
    // Interface containing сurd methods for Tasks repository
    public interface ITasksRepository
    {
        public IQueryable<Task> GetAll();
        public System.Threading.Tasks.Task<Task> Read(int id);
        public System.Threading.Tasks.Task<Task> Create(Task item);
        public System.Threading.Tasks.Task<Task> Delete(int id);
        public System.Threading.Tasks.Task<Task> Update(Task item);
    }
}
