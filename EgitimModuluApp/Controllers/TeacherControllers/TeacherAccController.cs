using AutoMapper;
using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos;
using Entities.Dtos.TeacherDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.TeacherControllers
{
    [Route("api/teacher/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.Teacher)]
    public class TeacherAccController : BaseController
    {
        private readonly IDataAccessContext _dataAccess;
        private readonly IMapper _mapper;

        public TeacherAccController(IDataAccessContext dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        [HttpPut, Route("profile")]
        public async Task<ActionResult> PutProfile([FromBody] TeacherDto teacherDto)
        {
            teacherDto.Id = Account.Id;
            teacherDto.SchoolId = Account.SchoolId;
            await _dataAccess.TeacherAccess().updateTeacher(_mapper.Map<Teacher>(teacherDto));
            return NoContent();
        }
    }
}
