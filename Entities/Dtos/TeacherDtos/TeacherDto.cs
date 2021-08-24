using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.TeacherDtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(250)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Tel { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public int SchoolId { get; set; } = -1;
        public Role Role
        {
            get
            {
                return Role.Undefined;
            }
        }
        private List<TeacherDto> items = new List<TeacherDto>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

    }
}