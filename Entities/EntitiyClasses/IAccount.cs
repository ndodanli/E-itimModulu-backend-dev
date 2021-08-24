using System.Collections.Generic;

namespace Entities
{
    public interface IAccount
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        Role Role { get; set; }
        RefreshToken RefreshToken { get; set; }
    }
}