using AkvelonTestTask.Domain.Repositories.Abstract;
using AkvelonTestTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AkvelonTestTask.Domain.Repositories.EntityFramework
{
    // Implement CRUD Functionality with the Entity Framework for Tasks repository
    public class EFTasksRepository: ITasksRepository
    {
        private readonly AppDbContext context;

        public EFTasksRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Task> GetAll()
        {
            return context.Tasks.AsQueryable();
        }

        public async System.Threading.Tasks.Task<Task> Read(int id)
        {
            return await context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async System.Threading.Tasks.Task<Task> Create(Task item)
        {
            var result = await context.Tasks.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async System.Threading.Tasks.Task<Task> Delete(int id)
        {

            var result = await context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (result != null)
            {
                context.Tasks.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async System.Threading.Tasks.Task<Task> Update(Task item)
        {
            var result = await context.Tasks.FirstOrDefaultAsync(t => t.Id == item.Id);

            if (result != null)
            {
                result.Name = item.Name;
                result.Priority = item.Priority;
                result.ProjectId = item.ProjectId;
                result.Status = item.Status;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
