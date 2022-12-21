using AkvelonTestTask.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTestTask.Domain.Repositories.Abstract
{

    // Interface containing сurd methods for Projects repository
    public interface IProjectsRepository
    {
        public IQueryable<Project> GetAll();
        public Task<Project> Read(int id);
        public Task<Project> Create(Project item);
        public Task<Project> Delete(int id);
        public Task<Project> Update(Project item);
    }
}
