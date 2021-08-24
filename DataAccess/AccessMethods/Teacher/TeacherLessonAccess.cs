using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos.LessonDtos;

namespace DataAccess.AccessMethods
{
    public class TeacherLessonAccess
    {
        public TeacherLessonAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;
        public async Task<(List<LessonListDto>, int)> getLessonsByPage(int teacherId, int schoolId, int? page, int? count)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            int totalItemCount = await _Context.Lessons
            .Where(a => a.TeacherId == teacherId && a.SchoolId == schoolId).CountAsync();
            List<LessonListDto> value = await _Context.Lessons
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId)
            .OrderBy(p => p.Id)
            .Select(p => new LessonListDto
            {
                Id = p.Id,
                Name = p.Name
            })
            .Skip((count * page) ?? 0)
                            .Take(count ?? 50)
                          .ToListAsync();
            return (value, totalItemCount);
        }
        public async Task<(List<LessonListDto>, int)> getLessonsByQuery(int teacherId, int schoolId, int? page, int? count, string query)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            query = query.ToLower();
            int totalItemCount = await _Context.Lessons
            .Where(a => a.TeacherId == teacherId && a.SchoolId == schoolId && a.Name.ToLower().Contains(query)).CountAsync();
            List<LessonListDto> value = await _Context.Lessons
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.Name.ToLower().Contains(query))
            .OrderBy(p => p.Id)
            .Select(p => new LessonListDto
            {
                Id = p.Id,
                Name = p.Name,
            })
            .Skip((count * page) ?? 0)
                            .Take(count ?? 50)
                          .ToListAsync();
            return (value, totalItemCount);
        }

        public async Task<Lesson> getLessonById(int lessonId, int schoolId)
        {
            return await _Context.Lessons.FirstAsync(ls => ls.Id == lessonId && ls.SchoolId == schoolId);
        }

        public async Task<Lesson> addLesson(Lesson lesson)
        {
            await _Context.Lessons.AddAsync(lesson);
            await _Context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> updateLesson(Lesson newLesson)
        {
            Lesson lesson = await _Context.Lessons
            .FirstOrDefaultAsync(a => a.Id == newLesson.Id && a.SchoolId == newLesson.SchoolId);

            if (lesson != null)
            {
                UtilityMethods.UpdateProps<Lesson>(lesson, newLesson);
            }

            _Context.Update(lesson);
            await _Context.SaveChangesAsync();
            return lesson;
        }

        public async Task deleteLesson(Lesson lesson)
        {
            _Context.Lessons.Remove(lesson);
            await _Context.SaveChangesAsync();
        }
    }
}