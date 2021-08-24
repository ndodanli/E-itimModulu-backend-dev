namespace Entities.Dtos.AccountDtos
{
    public class RefreshJwtDto
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}