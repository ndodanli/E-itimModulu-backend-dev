using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Teacher : TimeFieldsWithDeclaration, IAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public string EmailAddress { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public Role Role { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
        public RefreshToken RefreshToken { get; set; }
        private List<Teacher> items = new List<Teacher>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}