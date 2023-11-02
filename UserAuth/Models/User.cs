using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BornDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CPF { get; set; }
        public User() : base()
        {
            
        }
    }
}
