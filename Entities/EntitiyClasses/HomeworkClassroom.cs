using System;
using System.Collections.Generic;

namespace Entities
{
    public class HomeworkClassroom : TimeFieldsWithoutDeclaration
    {
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
    }
}
