using System;
using System.Collections.Generic;

namespace Entities
{
    public class ClassroomLesson
    {
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
