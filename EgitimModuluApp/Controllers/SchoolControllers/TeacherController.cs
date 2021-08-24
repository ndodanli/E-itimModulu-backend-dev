using System.Collections.Generic;
using System.Threading.Tasks;
using EgitimModuluApp.DataAccessLayer;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Dtos;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Entities.Dtos.TeacherDtos;

namespace EgitimModuluApp.Controllers.SchoolControllers
{
    [Route("api/school/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.School)]
    public class TeacherController : BaseController
    {
        private readonly DataAccessContext _dataAccess;
        private readonly ILogger<TeacherController> _logger;
        private readonly IMapper _mapper;

        private readonly DataContext _Context;
        public TeacherController(DataContext context, ILogger<TeacherController> logger, IMapper mapper)
        {
            _dataAccess = new DataAccessContext(context);
            _logger = logger;
            _mapper = mapper;
            _Context = context;
        }
        [HttpGet, Route("list")]
        public async Task<ActionResult<(List<TeacherDto>, int)>> GetTeacherList()
        {
            var teachers = await _dataAccess.SchoolTeacherAccess().getTeachersById(Account.Id);
            return Ok(teachers);

        }
        [HttpGet]
        public async Task<ActionResult<(List<Teacher>, int)>> GetTeachersByPage([FromQuery] int page, int count, string query)
        {
            if (query != null)
            {
                var (teachers, totalItemCount) = await _dataAccess.SchoolTeacherAccess().getTeachersByQuery(Account.Id, page, count, query);
                return Ok(new { teachers, totalItemCount });
            }
            else
            {
                var (teachers, totalItemCount) = await _dataAccess.SchoolTeacherAccess().getTeachersByPage(Account.Id, page, count);
                return Ok(new { teachers, totalItemCount });
            }
        }

        [HttpGet, Route("username")]
        public async Task<ActionResult> GetCheckUsername([FromQuery] string username)
        {
            bool isExist = await _dataAccess.SchoolTeacherAccess().checkUsername(username, Account.Id);
            return Ok(isExist);
        }

        [HttpPost]
        public async Task<ActionResult> PostTeacher([FromBody] TeacherDto teacherDto)
        {
            teacherDto.SchoolId = Account.Id;
            Teacher teacher = await _dataAccess.SchoolTeacherAccess().addTeacher(_mapper.Map<Teacher>(teacherDto));
            return Created(string.Empty, teacher.Id);
        }

        [HttpPut]
        public async Task<ActionResult> PutTeacher([FromBody] TeacherDto teacherDto)
        {
            teacherDto.SchoolId = Account.Id;
            await _dataAccess.SchoolTeacherAccess().updateTeacher(_mapper.Map<Teacher>(teacherDto));
            return NoContent();

        }

        [Authorize(Role.Admin, Role.School)]
        [HttpDelete]
        public async Task<ActionResult<Teacher>> DeleteTeacher([FromQuery] int teacherId)
        {
            Teacher teacher = await _dataAccess.SchoolTeacherAccess().getTeacherById(teacherId, Account.Id);
            await _dataAccess.SchoolTeacherAccess().deleteTeacher(teacher);
            return NoContent();
        }
    }
}