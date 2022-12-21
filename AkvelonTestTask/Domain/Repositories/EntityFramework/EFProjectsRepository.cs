using AkvelonTestTask.Domain.Repositories.Abstract;
using AkvelonTestTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonTestTask.Domain.Repositories.EntityFramework
{
    // Implement CRUD Functionality with the Entity Framework for Projects Repository
    public class EFProjectsRepository: IProjectsRepository
    {
        private readonly AppDbContext context;

        public EFProjectsRepository(AppDbContext ctx)
        {
            context = ctx;

        }
        public IQueryable<Project> GetAll()
        {
            return context.Projects.AsQueryable();
        }

        public async Task<Project> Read(int id)
        {
            return await context.Projects.Include(p=>p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> Create(Project item)
        {
            var result = await context.Projects.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Project> Delete(int id)
        {
            var result = await context.Projects.FirstOrDefaultAsync(t => t.Id == id);
            if (result != null)
            {
                context.Projects.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Project> Update(Project item)
        {
            var result = await context.Projects.FirstOrDefaultAsync(t => t.Id == item.Id);

            if (result != null)
            {
                result.Name = item.Name;
                result.Priority = item.Priority;
                result.Status = item.Status;
                result.StartDate = item.StartDate;
                result.CompletionDate = item.CompletionDate;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

    }
}
