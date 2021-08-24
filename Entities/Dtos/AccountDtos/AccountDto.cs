namespace Entities.Dtos.AccountDtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Tel { get; set; }
        public string Name { get; set; }
        public Payment Payment { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ClassroomName { get; set; }
        public string SchoolNumber { get; set; }
        public int Age { get; set; }
        public int? Grade { get; set; }
        public int SchoolId { get; set; }
        public int? ClassroomId { get; set; }
    }
}