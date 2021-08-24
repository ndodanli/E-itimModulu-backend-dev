using AutoMapper;
using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos;
using Entities.Dtos.StudentDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.SchoolControllers
{
    [Route("api/school/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.School)]
    public class StudentController : BaseController
    {
        private readonly IDataAccessContext _dataAccess;
        private readonly IMapper _mapper;

        public StudentController(IDataAccessContext dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<(List<StudentListDto>, int)>> GetStudentsByPage([FromQuery] int page, int count, string query)
        {
            if (query != null)
            {
                var (students, totalItemCount) = await _dataAccess.SchoolStudentAccess().getStudentsByQuery(Account.Id, page, count, query);
                return Ok(new { students, totalItemCount });
            }
            else
            {
                var (students, totalItemCount) = await _dataAccess.SchoolStudentAccess().getStudentsByPage(Account.Id, page, count);
                return Ok(new { students, totalItemCount });
            }
        }

        [HttpGet, Route("username")]
        public async Task<ActionResult> GetCheckUsername([FromQuery] string username)
        {
            bool isExist = await _dataAccess.SchoolStudentAccess().checkUsername(username, Account.Id);
            return Ok(isExist);
        }

        [HttpPost]
        public async Task<ActionResult> PostStudent([FromBody] StudentDto studentDto)
        {
            studentDto.SchoolId = Account.Id;
            Student student = await _dataAccess.SchoolStudentAccess().addStudent(_mapper.Map<Student>(studentDto));
            return Created(string.Empty, student.Id); //sadece id gitmesi için değiştirildi
        }

        [HttpPut]
        public async Task<ActionResult> PutStudent([FromBody] StudentDto studentDto)
        {
            studentDto.SchoolId = Account.Id;
            await _dataAccess.SchoolStudentAccess().updateStudent(_mapper.Map<Student>(studentDto));
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteStudent([FromQuery] int studentId)
        {
            Student student = await _dataAccess.SchoolStudentAccess().getStudentById(studentId, Account.Id);
            await _dataAccess.SchoolStudentAccess().deleteStudent(student);
            return NoContent();
        }
    }
}
