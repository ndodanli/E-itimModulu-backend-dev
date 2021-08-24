using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Classroom : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Homework> Homeworks { get; set; }

    }
}