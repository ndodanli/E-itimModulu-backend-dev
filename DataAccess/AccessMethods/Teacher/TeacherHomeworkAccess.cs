using Entities;
using Entities.Dtos.HomeworkDtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.AccessMethods
{
    public class TeacherHomeworkAccess
    {
        private DataContext _Context;
        public TeacherHomeworkAccess(DataContext context)
        {
            _Context = context;
        }

        public async Task<(List<HomeworkListDto>, int)> getHomeworksByPage(int teacherId, int lessonId, int schoolId, int? page, int? count)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            int totalItemCount = await _Context.Homeworks
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId).CountAsync();
            List<HomeworkListDto> value = await _Context.Homeworks
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId)
            .OrderBy(p => p.Id)
            .Select(p => new HomeworkListDto
            {
                Id = p.Id,
                Name = p.Name,
                Status = p.Status
            })
            .Skip((count * page) ?? 0)
            .Take(count ?? 50)
            .ToListAsync();
            return (value, totalItemCount);
        }

        public async Task<(List<HomeworkListDto>, int)> getHomeworksByQuery(int teacherId, int lessonId, int schoolId, int? page, int? count, string query)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            query = query.ToLower();
            int totalItemCount = await _Context.Homeworks
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId &&
            (
                p.Name.Contains(query)
            )).CountAsync();
            List<HomeworkListDto> value = await _Context.Homeworks
                            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId &&
                             (
                                p.Name.Contains(query)
                             ))
                            .OrderBy(p => p.Id)
                            .Select(p => new HomeworkListDto
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Status = p.Status
                            })
                            .Skip((count * page) ?? 0)
                            .Take(count ?? 50)
                          .ToListAsync();
            return (value, totalItemCount);
        }

        public async Task<Homework> getHomeworkById(int homeworkId, int schoolId)
        {
            return await _Context.Homeworks.FirstAsync(ls => ls.Id == homeworkId && ls.SchoolId == schoolId);
        }

        public async Task<Homework> addHomework(Homework homework)
        {
            await _Context.Homeworks.AddAsync(homework);
            await _Context.SaveChangesAsync();
            return homework;
        }

        public async Task<Homework> updateHomework(Homework newHomework)
        {
            Homework homework = await _Context.Homeworks
            .FirstOrDefaultAsync(a => a.Id == newHomework.Id && a.TeacherId == newHomework.TeacherId);

            if (homework != null)
            {
                UtilityMethods.UpdateProps<Homework>(homework, newHomework);
            }

            _Context.Update(homework);
            await _Context.SaveChangesAsync();
            return homework;
        }

        public async Task deleteHomework(Homework homework)
        {
            _Context.Homeworks.Remove(homework);
            await _Context.SaveChangesAsync();
        }
    }
}
