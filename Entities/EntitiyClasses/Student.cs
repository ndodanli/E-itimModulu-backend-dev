using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Student : TimeFieldsWithDeclaration, IAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public string SchoolNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Grade { get; set; }
        public Classroom Classroom { get; set; }
        [ForeignKey("Classroom")]
        public int? ClassroomId { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public Role Role { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        private List<Student> items = new List<Student>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}