using ApiPeople.Context;
using ApiPeople.Controllers;
using ApiPeople.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPeople.Services
{
    public class UserService : IUserService
    {
        AppDbContext context;

        public UserService(AppDbContext dbcontext)
        {
            context = dbcontext;
        }

        public UserService()
        {
        }
        
        public IEnumerable<GetUserDto> Get()
        {
            return context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new GetUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToList();
        }

        public async System.Threading.Tasks.Task Save(User User, string roleName)
        {
            // Buscar el rol por nombre
            var role = await context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                throw new ArgumentException($"Role '{roleName}' not found.");
            }

            // Agregar el usuario
            context.Users.Add(User);
            await context.SaveChangesAsync();

            // Crear la relación UserRole
            var userRole = new UserRole
            {
                UserId = User.Id,
                RoleId = role.Id
            };
            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Update(int id, UserDto userDto)
        {
            // Buscar el usuario por Id
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"User with Id '{id}' not found.");
            }

            // Actualizar los campos del usuario
            user.Name = userDto.Name;
            user.Email = userDto.Email;

            // Buscar el nuevo rol por nombre
            var role = await context.Roles.SingleOrDefaultAsync(r => r.Name == userDto.RoleName);
            if (role == null)
            {
                throw new ArgumentException($"Role '{userDto.RoleName}' not found.");
            }

            // Actualizar los roles del usuario
            user.UserRoles.Clear();
            user.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            // Guardar los cambios
            await context.SaveChangesAsync();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Username == username);

            // Verificar si el usuario existe
            if (user == null)
                return null;

            // Verificar si la contraseña es correcta
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Autenticación exitosa
            return user;
        }
    }

    public interface IUserService
    {
        IEnumerable<GetUserDto> Get();

        System.Threading.Tasks.Task Save(User User, string roleName);

        System.Threading.Tasks.Task Update(int id, UserDto userDto);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        
        System.Threading.Tasks.Task<User> Authenticate(string username, string password);

    }
}
