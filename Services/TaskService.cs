using ApiPeople.Context;
using ApiPeople.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiPeople.Services
{
    public class TaskService : ITaskService
    {
        AppDbContext context;

        public TaskService(AppDbContext dbcontext)
        {
            context = dbcontext;
        }

        public IEnumerable<ApiPeople.Models.Task> Get()
        {
            return context.Task;
        }

        public async System.Threading.Tasks.Task Save(ApiPeople.Models.Task task)
        {
            task.TaskId = Guid.NewGuid();
            task.created_at = DateTime.Now;
            await context.AddAsync(task);

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Update(Guid id, ApiPeople.Models.Task task)
        {

            var currentTaskl = context.Task.Find(id);

            if (currentTaskl != null)
            {
                currentTaskl.CategoryId = task.CategoryId;
                currentTaskl.Tittle = task.Tittle;
                currentTaskl.Priority = task.Priority;
                currentTaskl.Description = task.Description;

                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            var currentTaskl = context.Task.Find(id);

            if (currentTaskl != null)
            {
                context.Remove(currentTaskl);
                await context.SaveChangesAsync();

            }

        }
    }

    public interface ITaskService
    {
        IEnumerable<ApiPeople.Models.Task> Get();
        System.Threading.Tasks.Task Save(ApiPeople.Models.Task Tarea);

        System.Threading.Tasks.Task Update(Guid id, ApiPeople.Models.Task Tarea);

        System.Threading.Tasks.Task Delete(Guid id);
    }
}
