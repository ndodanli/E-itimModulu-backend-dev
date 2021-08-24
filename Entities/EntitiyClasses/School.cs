using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities
{
    public class School : TimeFieldsWithDeclaration, IAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tel { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Code { get; set; }
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<MediaFile> MediaFiles { get; set; }
        public Role Role { get; set; }
        public RefreshToken RefreshToken { get; set; }
        private List<School> items = new List<School>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}