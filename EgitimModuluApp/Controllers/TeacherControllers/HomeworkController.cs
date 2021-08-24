using AutoMapper;
using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos.HomeworkDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.TeacherControllers
{
    [Route("api/teacher/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.Teacher)]
    public class HomeworkController : BaseController
    {

        private readonly IDataAccessContext _dataAccess;
        private readonly IMapper _mapper;

        public HomeworkController(IDataAccessContext dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<(List<HomeworkListDto>, int)>> GetHomeworksByPage([FromQuery] int lessonId, int page, int count, string query)
        {
            if (query != null)
            {
                var (homeworks, totalItemCount) = await _dataAccess.TeacherHomeworkAccess().getHomeworksByQuery(Account.Id, lessonId, Account.SchoolId, page, count, query);
                return Ok(new { homeworks, totalItemCount });
            }
            else
            {
                var (homeworks, totalItemCount) = await _dataAccess.TeacherHomeworkAccess().getHomeworksByPage(Account.Id, lessonId, Account.SchoolId, page, count);
                return Ok(new { homeworks, totalItemCount });
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostHomework([FromForm] HomeworkDto homeworkDto)
        {
            // File yüklendiyse
            if (homeworkDto.File != null)
            {
                homeworkDto.TeacherId = Account.Id;
                homeworkDto.SchoolId = Account.SchoolId;
                homeworkDto.FilePath = Helpers.FileHelpers.UploadedFile(homeworkDto.File);
                Homework homework = await _dataAccess.TeacherHomeworkAccess().addHomework(_mapper.Map<Homework>(homeworkDto));
                return Created(string.Empty, homework.Id);
            }
            else
            {
                // File yüklenmediyse
                homeworkDto.TeacherId = Account.Id;
                homeworkDto.SchoolId = Account.SchoolId;
                Homework homework = await _dataAccess.TeacherHomeworkAccess().addHomework(_mapper.Map<Homework>(homeworkDto));
                return Created(string.Empty, homework.Id);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutHomework([FromForm] HomeworkDto homeworkDto)
        {
            // File yüklendiyse
            if (homeworkDto.File != null)
            {
                // Veritabanı üzerinden varolan homework verisini getirip içinden filepath alıyoruz.
                Homework existingHomework = await _dataAccess.TeacherHomeworkAccess().getHomeworkById(homeworkDto.Id, Account.Id);
                // Veriyi depolandığı yerde bulup siliyoruz.
                if (existingHomework.FilePath != null)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingHomework.FilePath);
                    System.IO.File.Delete(filePath);
                }
                // Yeni Yüklenen dosyayı yeni filepath olarak belirtiyoruz.
                homeworkDto.TeacherId = Account.Id;
                homeworkDto.FilePath = Helpers.FileHelpers.UploadedFile(homeworkDto.File);
                await _dataAccess.TeacherHomeworkAccess().updateHomework(_mapper.Map<Homework>(homeworkDto));
                return NoContent();
            }
            else
            {
                // File yüklenmediyse
                homeworkDto.TeacherId = Account.Id;
                await _dataAccess.TeacherHomeworkAccess().updateHomework(_mapper.Map<Homework>(homeworkDto));
                return NoContent();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteHomework([FromQuery] int homeworkId)
        {
            Homework homework = await _dataAccess.TeacherHomeworkAccess().getHomeworkById(homeworkId, Account.Id);
            await _dataAccess.TeacherHomeworkAccess().deleteHomework(homework);
            return NoContent();
        }
    }
}
