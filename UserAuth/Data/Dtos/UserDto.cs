using System.ComponentModel.DataAnnotations;

namespace UserAuth.Data.Dtos
{
    public class UserDto
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
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}
