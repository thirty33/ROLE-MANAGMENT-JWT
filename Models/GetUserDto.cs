﻿namespace ApiPeople.Models
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
