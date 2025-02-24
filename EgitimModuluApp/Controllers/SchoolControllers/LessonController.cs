﻿using AutoMapper;
using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos.LessonDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.SchoolControllers
{
    [Route("api/school/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.School)]
    public class LessonController : BaseController
    {
        private readonly IDataAccessContext _dataAccess;
        private readonly IMapper _mapper;

        public LessonController(IDataAccessContext dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<(List<LessonListDto>, int)>> GetLessonsByPage([FromQuery] int page, int count, string query)
        {
            if (query != null)
            {
                var (lessons, totalItemCount) = await _dataAccess.SchoolLessonAccess().getLessonsByQuery(Account.Id, page, count, query);
                return Ok(new { lessons, totalItemCount });
            }
            else
            {
                var (lessons, totalItemCount) = await _dataAccess.SchoolLessonAccess().getLessonsByPage(Account.Id, page, count);
                return Ok(new { lessons, totalItemCount });
            }
        }

        [HttpGet, Route("lessoncode")]
        public async Task<ActionResult> GetCheckLessonCode([FromQuery] string lessonCode)
        {
            bool isExist = await _dataAccess.SchoolLessonAccess().checkLessonCode(lessonCode, Account.Id);
            return Ok(isExist);
        }

        [HttpPost]
        public async Task<ActionResult> PostLesson([FromBody] LessonDto lessonDto)
        {
            lessonDto.SchoolId = Account.Id;
            Lesson lesson = await _dataAccess.SchoolLessonAccess().addLesson(_mapper.Map<Lesson>(lessonDto));
            return Created(string.Empty, lesson.Id);
        }

        [HttpPut]
        public async Task<ActionResult> PutLesson([FromBody] LessonDto lessonDto)
        {
            lessonDto.SchoolId = Account.Id;
            await _dataAccess.SchoolLessonAccess().updateLesson(_mapper.Map<Lesson>(lessonDto));
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLesson([FromQuery] int lessonId)
        {
            Lesson lesson = await _dataAccess.SchoolLessonAccess().getLessonById(lessonId, Account.Id);
            await _dataAccess.SchoolLessonAccess().deleteLesson(lesson);
            return NoContent();
        }
    }
}
