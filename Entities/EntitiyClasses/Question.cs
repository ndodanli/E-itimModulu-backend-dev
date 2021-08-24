using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Question : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ExamQuestion { get; set; }
        public string Answer { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        private List<Question> items = new List<Question>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

    }
}