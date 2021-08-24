using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Exam : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TimeSpan TotalTime { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        public Lesson Lesson { get; set; }
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Student> Students { get; set; }
        private List<Exam> items = new List<Exam>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

    }
}