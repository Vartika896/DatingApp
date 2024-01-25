using System.ComponentModel.DataAnnotations;

namespace TestAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8,MinimumLength =5)]
        public string  Password { get; set; }
    }
}