using System;
using System.Collections.Generic;

namespace Entities
{
    public class HomeworkStudent : TimeFieldsWithoutDeclaration
    {
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string FilePath { get; set; }
    }
}
