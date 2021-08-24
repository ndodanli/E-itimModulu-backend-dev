using System;
using System.Collections.Generic;

namespace Entities
{
    public class StudentExam
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

    }
}