using Entities.Dtos.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.HomeworkDtos
{
    public class HomeworkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public bool Status { get; set; }
        public int? TeacherId { get; set; }
        public int LessonId { get; set; } = -1;
        public int SchoolId { get; set; } = -1;
    }
}
