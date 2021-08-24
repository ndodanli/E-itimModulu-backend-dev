using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.LessonDtos
{
    public class LessonListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LessonCode { get; set; }
        public int TeacherId { get; set; }
    }
}
