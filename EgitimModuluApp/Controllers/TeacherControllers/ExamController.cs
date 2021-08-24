using AutoMapper;
using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos.ExamDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.TeacherControllers
{
    [Route("api/teacher/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.Teacher)]
    public class ExamController : BaseController
    {
        private readonly IDataAccessContext _dataAccess;
        private readonly IMapper _mapper;

        public ExamController(IDataAccessContext dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<(List<ExamListDto>, int)>> GetExamsByPage([FromQuery] int lessonId, int page, int count, string query)
        {
            if (query != null)
            {
                var (exams, totalItemCount) = await _dataAccess.TeacherExamAccess().getExamsByQuery(Account.Id, lessonId, Account.SchoolId, page, count, query);
                return Ok(new { exams, totalItemCount });
            }
            else
            {
                var (exams, totalItemCount) = await _dataAccess.TeacherExamAccess().getExamsByPage(Account.Id, lessonId, Account.SchoolId, page, count);
                return Ok(new { exams, totalItemCount });
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostExam([FromForm] ExamDto examDto)
        {
            // File yüklendiyse
            if (examDto.File != null)
            {
                examDto.TeacherId = Account.Id;
                examDto.SchoolId = Account.SchoolId;
                examDto.FilePath = Helpers.FileHelpers.UploadedFile(examDto.File);
                Exam exam = await _dataAccess.TeacherExamAccess().addExam(_mapper.Map<Exam>(examDto));
                return Created(string.Empty, exam.Id);
            }
            else
            {
                // File yüklenmediyse
                examDto.TeacherId = Account.Id;
                examDto.SchoolId = Account.SchoolId;
                Exam exam = await _dataAccess.TeacherExamAccess().addExam(_mapper.Map<Exam>(examDto));
                return Created(string.Empty, exam.Id);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutExam([FromForm] ExamDto examDto)
        {
            // File yüklendiyse
            if (examDto.File != null)
            {
                // Veritabanı üzerinden varolan homework verisini getirip içinden filepath alıyoruz.
                Exam existingExam = await _dataAccess.TeacherExamAccess().getExamById(examDto.Id, Account.Id);
                // Veriyi depolandığı yerde bulup siliyoruz.
                if (existingExam.FilePath != null)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingExam.FilePath);
                    System.IO.File.Delete(filePath);
                }
                // Yeni Yüklenen dosyayı yeni filepath olarak belirtiyoruz.
                examDto.TeacherId = Account.Id;
                examDto.FilePath = Helpers.FileHelpers.UploadedFile(examDto.File);
                await _dataAccess.TeacherExamAccess().updateExam(_mapper.Map<Exam>(examDto));
                return NoContent();
            }
            else
            {
                // File yüklenmediyse
                examDto.TeacherId = Account.Id;
                await _dataAccess.TeacherExamAccess().updateExam(_mapper.Map<Exam>(examDto));
                return NoContent();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteExam([FromQuery] int examId)
        {
            Exam exam = await _dataAccess.TeacherExamAccess().getExamById(examId, Account.Id);
            await _dataAccess.TeacherExamAccess().deleteExam(exam);
            return NoContent();
        }
    }
}