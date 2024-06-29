using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using ApiPeople.Models;
using System.Threading.Tasks;
using ApiPeople.Services;


namespace ApiPeople.Context
{
    public class AppDbContext : DbContext 
    {
        IUserService UserService;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            UserService = new UserService();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<ApiPeople.Models.Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> CategoriesInit = new List<Category>();

            CategoriesInit.Add(new Category() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Name = "Actividades pendientes", Weight = 20, Description = "hola" });
            CategoriesInit.Add(new Category() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Name = "Actividades personales", Weight = 50, Description = "hola" });


            modelBuilder.Entity<Category>(category =>
            {
                category.ToTable("Category");
                category.HasKey(p => p.CategoryId);

                category.Property(p => p.Name).IsRequired().HasMaxLength(150);

                category.Property(p => p.Description).IsRequired(false);

                category.Property(p => p.Weight);

                //category.HasMany(p => p.Tasks).WithOne(p => p.Category);

                category.HasData(CategoriesInit);
            });

            List<ApiPeople.Models.Task> TasksInit = new List<ApiPeople.Models.Task>();

            TasksInit.Add(new ApiPeople.Models.Task() { TaskId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Priority = Priority.Low, Tittle = "Pago de servicios publicos", created_at = DateTime.Now });
            TasksInit.Add(new ApiPeople.Models.Task() { TaskId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Priority = Priority.Low, Tittle = "Terminar de ver pelicula en netflix", created_at = DateTime.Now });

            modelBuilder.Entity<ApiPeople.Models.Task>(task =>
            {
                task.ToTable("task");
                task.HasKey(p => p.TaskId);

                task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);

                task.Property(p => p.Tittle).IsRequired().HasMaxLength(200);

                task.Property(p => p.Description).IsRequired(false);

                task.Property(p => p.Priority);

                task.Property(p => p.created_at);

                task.Ignore(p => p.Summary);

                task.HasData(TasksInit);

            });


            //Users adn Roles

            var users = new List<User>()
            {
                new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Username = "John" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Username = "Jane" },
            };

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                UserService.CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(p => p.Id);
                user.Property(p => p.Name).IsRequired().HasMaxLength(200);
                user.Property(p => p.Username).IsRequired().HasMaxLength(25);
                user.Property(p => p.Email).IsRequired().HasMaxLength(50);
                user.Property(p => p.PasswordHash).IsRequired();
                user.Property(p => p.PasswordSalt).IsRequired();
                user.HasData(users);
            });


            var roles = new List<Role>()
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Customer" },
            };

            modelBuilder.Entity<Role>(role =>
            {
                role.HasKey(p => p.Id);
                role.Property(p => p.Name).IsRequired().HasMaxLength(20);
                role.HasData(roles);
            });


            var userRolesData = new List<UserRole>()
            {
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 2 },
                new UserRole { UserId = 1, RoleId = 2 },
            };

            modelBuilder.Entity<UserRole>(user_role => {

                user_role.HasKey(sc => new { sc.UserId, sc.RoleId });

                user_role
                    .HasOne<User>(sc => sc.User)
                    .WithMany(s => s.UserRoles)
                    .HasForeignKey(sc => sc.UserId);

                user_role
                    .HasOne<Role>(sc => sc.Role)
                    .WithMany(s => s.UserRoles)
                    .HasForeignKey(sc => sc.RoleId);

                user_role.HasData(userRolesData);
            });
            
        }
    }
}
