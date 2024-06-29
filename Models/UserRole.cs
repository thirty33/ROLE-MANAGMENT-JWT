using System.Text.Json.Serialization;

namespace ApiPeople.Models
{
    public class UserRole
    {
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;

        public int RoleId { get; set; }

        [JsonIgnore]
        public Role Role { get; set; } = null!;
    }
}
