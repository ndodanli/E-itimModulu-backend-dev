using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.AccountDtos
{
    public class AuthReqDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string SchoolCode { get; set; }
        [Required]
        public Role AccountType { get; set; }
    }
}