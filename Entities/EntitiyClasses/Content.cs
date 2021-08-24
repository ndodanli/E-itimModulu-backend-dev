using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Content : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public bool Status { get; set; }
        public Lesson Lesson { get; set; }
        [ForeignKey("Lesson")]
        public int? LessonId { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public ICollection<MediaFile> MediaFiles { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
    }
}