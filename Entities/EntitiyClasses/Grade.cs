using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Grade : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        private List<Grade> items = new List<Grade>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

    }
}