using System.Collections.Generic;
using System.Threading.Tasks;
using EgitimModuluApp.DataAccessLayer;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Dtos;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Http;
using Entities.Dtos.ClassroomDtos;

namespace EgitimModuluApp.Controllers.SchoolControllers
{
    [Route("api/school/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.School)]
    public class ClassroomController : BaseController
    {
        private readonly DataAccessContext _dataAccess;
        private readonly ILogger<ClassroomController> _logger;
        private readonly IMapper _mapper;

        private readonly DataContext _Context;
        public ClassroomController(DataContext context, ILogger<ClassroomController> logger, IMapper mapper)
        {
            _dataAccess = new DataAccessContext(context);
            _logger = logger;
            _mapper = mapper;
            _Context = context;
        }

        [HttpGet, Route("list")]
        public async Task<ActionResult<(List<ClassroomDto>, int)>> GetClassroomList()
        {
            var classrooms = await _dataAccess.SchoolClassroomAccess().getClassroomsById(Account.Id);
            return Ok(classrooms);

        }
        [HttpGet]
        public async Task<ActionResult<(List<ClassroomDto>, int)>> GetClassroomsByPage([FromQuery] int page, int count, string query)
        {
            if (query != null)
            {
                var (classrooms, totalItemCount) = await _dataAccess.SchoolClassroomAccess().getClassroomsByQuery(Account.Id, page, count, query);
                return Ok(new { classrooms, totalItemCount });
            }
            else
            {
                var (classrooms, totalItemCount) = await _dataAccess.SchoolClassroomAccess().getClassroomsByPage(Account.Id, page, count);
                return Ok(new { classrooms, totalItemCount });
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostClassRoom([FromBody] ClassroomDto classroomDto)
        {
            classroomDto.SchoolId = Account.Id;
            Classroom classroom = await _dataAccess.SchoolClassroomAccess().addClassroom(_mapper.Map<Classroom>(classroomDto));
            return Created(string.Empty, classroom.Id); //sadece id gitmesi için değiştirildi

        }

        [HttpPut]
        public async Task<ActionResult> PutClassroom([FromBody] ClassroomDto classroomDto)
        {
            classroomDto.SchoolId = Account.Id;
            await _dataAccess.SchoolClassroomAccess().updateClassroom(_mapper.Map<Classroom>(classroomDto));
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<Classroom>> DeleteClassRoom([FromQuery] int classroomId)
        {
            Classroom classroom = await _dataAccess.SchoolClassroomAccess().getClassroomById(classroomId, Account.Id);
            await _dataAccess.SchoolClassroomAccess().deleteClassroom(classroom);
            return NoContent();
        }
    }
}