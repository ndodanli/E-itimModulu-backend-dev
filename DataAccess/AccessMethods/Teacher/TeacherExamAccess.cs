using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos.ExamDtos;

namespace DataAccess.AccessMethods
{
    public class TeacherExamAccess
    {
        public TeacherExamAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;
        public async Task<(List<ExamListDto>, int)> getExamsByPage(int teacherId, int lessonId, int schoolId, int? page, int? count)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            int totalItemCount = await _Context.Exams
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId).CountAsync();
            List<ExamListDto> value = await _Context.Exams
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId)
            .OrderBy(p => p.Id)
            .Select(p => new ExamListDto
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                DueDate = p.DueDate,
                TotalTime = p.TotalTime
            })
            .Skip((count * page) ?? 0)
                            .Take(count ?? 50)
                          .ToListAsync();
            return (value, totalItemCount);
        }
        public async Task<(List<ExamListDto>, int)> getExamsByQuery(int teacherId, int lessonId, int schoolId, int? page, int? count, string query)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            query = query.ToLower();
            int totalItemCount = await _Context.Exams
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId && p.Name.ToLower().Contains(query)).CountAsync();
            List<ExamListDto> value = await _Context.Exams
            .Where(p => p.TeacherId == teacherId && p.SchoolId == schoolId && p.LessonId == lessonId && p.Name.ToLower().Contains(query))
            .OrderBy(p => p.Id)
            .Select(p => new ExamListDto
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                DueDate = p.DueDate,
                TotalTime = p.TotalTime
            })
            .Skip((count * page) ?? 0)
                            .Take(count ?? 50)
                          .ToListAsync();
            return (value, totalItemCount);
        }

        public async Task<Exam> getExamById(int examId, int teacherId)
        {
            return await _Context.Exams.FirstAsync(ls => ls.Id == examId && ls.TeacherId == teacherId);
        }

        public async Task<Exam> addExam(Exam exam)
        {
            await _Context.Exams.AddAsync(exam);
            await _Context.SaveChangesAsync();
            return exam;
        }

        public async Task updateExam(Exam newExam)
        {
            Exam exam = await _Context.Exams
            .FirstOrDefaultAsync(p => p.Id == newExam.Id && p.TeacherId == newExam.TeacherId);

            if (exam != null)
            {
                UtilityMethods.UpdateProps<Exam>(exam, newExam);
            }

            _Context.Update(exam);
            await _Context.SaveChangesAsync();
        }

        public async Task deleteExam(Exam exam)
        {
            _Context.Exams.Remove(exam);
            await _Context.SaveChangesAsync();
        }
    }
}