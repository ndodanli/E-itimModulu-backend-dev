using System;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Dtos.StudentDtos
{
    ///<summary>
    ///<para><see cref="int"/> Id { get; set; } </para>
    ///<para><see cref="string"/> Username { get; set; } </para>
    ///<para><see cref="string"/> Password { get; set; } </para>
    ///<para><see cref="string"/> FirstName { get; set; } </para>
    ///<para><see cref="string"/> LastName { get; set; } </para>
    ///<para><see cref="string"/> Tel { get; set; } </para>
    ///<para><see cref="string"/> SchoolNumber { get; set; } </para>
    ///<para><see cref="int"/>? Age { get; set; } </para>
    ///<para><see cref="int"/>? Grade { get; set; } </para>
    ///<para><see cref="System.Array"/> ExamIds { get; set; } </para>
    ///<para><see cref="System.Array"/> LessonIds { get; set; } </para>
    ///<para><see cref="int"/>? ClassId { get; set; } </para>
    ///<para><see cref="int"/> Id { get; set; } </para>
    ///<para><see cref="int"/> Count { get; set; } </para>
    ///<para><see cref="string"/> Query { get; set; } </para>
    ///</summary>
    public class StudentDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public string SchoolNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Grade { get; set; }
        public int? ClassroomId { get; set; }
        public int SchoolId { get; set; } = -1;
        private List<StudentDto> items = new List<StudentDto>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}