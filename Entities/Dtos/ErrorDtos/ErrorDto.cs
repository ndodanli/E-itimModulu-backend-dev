using System.Collections.Generic;

namespace Entities.Dtos.ErrorDtos
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new Dictionary<string, IEnumerable<string>>();
        }
        //errorların key'leri olabileceğinden(örn. email validation error: {EmailAddress: "...error"})
        //Errors Dictionary olarak değiştirildi, ilgili exception buna göre düzenlendi.
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }
        public int Status { get; set; }
    }
}
