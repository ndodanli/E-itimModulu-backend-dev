using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Payment : TimeFieldsWithDeclaration
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public int MaxStudent { get; set; }
        public int MaxTeacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<School> Schools { get; set; }
        private List<Payment> items = new List<Payment>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}