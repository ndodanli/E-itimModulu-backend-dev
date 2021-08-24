using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class MediaFile : TimeFieldsWithDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public bool Status { get; set; }
        [Required]
        public Content Content { get; set; }
        [ForeignKey("Content")]
        public int ContentId { get; set; }
        [Required]
        public School School { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
    }
}