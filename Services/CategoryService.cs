using ApiPeople.Context;
using ApiPeople.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPeople.Services
{
    public class CategoryService : ICategoryService
    {
        AppDbContext context;

        public CategoryService(AppDbContext dbcontext)
        {
            context = dbcontext;
        }

        public IEnumerable<Category> Get()
        {
            return context.Categories;
        }

        public async System.Threading.Tasks.Task Save(Category Category)
        {
            context.Add(Category);
            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Update(Guid id, Category Category)
        {
            var CategoryActual = context.Categories.Find(id);

            if (CategoryActual != null)
            {
                CategoryActual.Name = Category.Name;
                Category.Description = Category.Description;
                Category.Weight = Category.Weight;

                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            var CategoryActual = context.Categories.Find(id);

            if (CategoryActual != null)
            {
                context.Remove(CategoryActual);
                await context.SaveChangesAsync();
            }
        }

    }
    
    public interface ICategoryService
    {
        IEnumerable<Category> Get();
        System.Threading.Tasks.Task Save(Category Category);

        System.Threading.Tasks.Task Update(Guid id, Category Category);

        System.Threading.Tasks.Task Delete(Guid id);
    }
}
