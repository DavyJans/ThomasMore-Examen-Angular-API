using AngularAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace AngularAPI.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        private Role Role { get; set; } = Role.Guest;
    }
}
