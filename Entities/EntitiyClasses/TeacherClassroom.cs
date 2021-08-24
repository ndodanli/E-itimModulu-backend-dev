using System;
using System.Collections.Generic;

namespace Entities
{
    public class TeacherClassroom
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}