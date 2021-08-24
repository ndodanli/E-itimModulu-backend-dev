using System;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos.ExamDtos
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public int? TeacherId { get; set; }
        public int LessonId { get; set; } = -1;
        public int SchoolId { get; set; } = -1;
    }
}
