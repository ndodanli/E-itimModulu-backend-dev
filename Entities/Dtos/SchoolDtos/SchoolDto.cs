using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Entities.Dtos.SchoolDtos
{
    public class SchoolDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(250)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Tel { get; set; }
        [MaxLength(50)]
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