using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class RefreshToken : TimeFieldsWithoutDeclaration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public School School { get; set; }
        [ForeignKey("School")]
        public int? SchoolId { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.Now >= Expires;
        public string CreatedByIp { get; set; }
        public bool IsActive => !IsExpired;
        private List<RefreshToken> items = new List<RefreshToken>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}