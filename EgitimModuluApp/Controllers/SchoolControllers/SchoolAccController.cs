using AutoMapper;
using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos;
using Entities.Dtos.SchoolDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.SchoolControllers
{
    [Route("api/school/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.School)]
    public class SchoolAccController : BaseController
    {
        private readonly IDataAccessContext _dataAccess;
        private readonly IMapper _mapper;

        public SchoolAccController(IDataAccessContext dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        [HttpPut, Route("profile")]
        public async Task<ActionResult> PutProfile([FromBody] SchoolDto schoolDto)
        {
            schoolDto.Id = Account.Id;
            await _dataAccess.SchoolAccess().updateSchool(_mapper.Map<School>(schoolDto));
            return NoContent();
        }
    }
}
