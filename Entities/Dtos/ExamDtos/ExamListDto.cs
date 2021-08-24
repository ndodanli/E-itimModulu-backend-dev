using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ExamDtos
{
    public class ExamListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
