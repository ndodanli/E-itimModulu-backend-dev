namespace Entities.Dtos.StudentDtos
{
    public class StudentListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel { get; set; }
        public string SchoolNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BirthDate { get; set; }
        public string CreatedAtDate { get; set; }
        public string UpdatedAtDate { get; set; }
        public string UpdatedAtTime { get; set; }
        public int? ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public int SchoolId { get; set; }
    }
}
