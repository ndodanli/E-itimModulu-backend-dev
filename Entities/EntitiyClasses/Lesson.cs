using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Lesson : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LessonCode { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
    }
}