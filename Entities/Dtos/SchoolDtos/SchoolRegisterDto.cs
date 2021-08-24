using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.SchoolDtos
{
    public class SchoolRegisterDto
    {
        [MaxLength(50), Required]
        public string Username { get; set; }
        [MaxLength(250), Required]
        public string Password { get; set; }
        [MaxLength(150), Required]
        public string EmailAddress { get; set; }
        [MaxLength(50), Required]
        public string Tel { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        public int PaymentId { get; set; } = -1;
        public Role Role
        {
            get { return Entities.Role.Undefined; }
        }
        private List<SchoolDto> items = new List<SchoolDto>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}