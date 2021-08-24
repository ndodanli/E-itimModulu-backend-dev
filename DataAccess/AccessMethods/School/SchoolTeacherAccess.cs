using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos;
using Entities.Dtos.TeacherDtos;

namespace DataAccess.AccessMethods
{
    public class SchoolTeacherAccess
    {
        public SchoolTeacherAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;
        public async Task<List<ClassTeacherDto>> getTeachersById(int schoolId)
        {
            return await _Context.Teachers
            .Where(p => p.SchoolId == schoolId)
            .OrderBy(p => p.Id)
            .Select(p => new ClassTeacherDto
            {
                TeacherId = p.Id,
                TeacherName = p.FirstName + " " + p.LastName,
                TeacherUsername = p.Username
            }).ToListAsync();
        }
        public async Task<(List<TeacherListDto>, int)> getTeachersByPage(int schoolId, int? page, int? count)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            int totalItemCount = await _Context.Teachers
            .Where(a => a.SchoolId == schoolId).CountAsync();
            List<TeacherListDto> value = await _Context.Teachers
            .Where(a => a.SchoolId == schoolId)
            .OrderBy(order => order.Id)
            .Select(t => new TeacherListDto
            {
                Id = t.Id,
                Username = t.Username,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Tel = t.Tel,
                EmailAddress = t.EmailAddress
            })
            .Skip((count * page) ?? 0)
            .Take(count ?? 50)
            .ToListAsync();
            return (value, totalItemCount);
        }
        public async Task<(List<TeacherListDto>, int)> getTeachersByQuery(int schoolId, int? page, int? count, string query)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            query = query.ToLower();
            int totalItemCount = await _Context.Teachers
            .Where(a => a.SchoolId == schoolId &&
            (
                a.FirstName.ToLower().Contains(query) || a.LastName.ToLower().Contains(query)
            )).CountAsync();
            List<TeacherListDto> value = await _Context.Teachers
            .OrderBy(order => order.Id)
            .Where(a => a.SchoolId == schoolId &&
            (
                a.FirstName.ToLower().Contains(query) || a.LastName.ToLower().Contains(query)
            ))
            .Select(t => new TeacherListDto
            {
                Id = t.Id,
                Username = t.Username,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Tel = t.Tel,
                EmailAddress = t.EmailAddress
            })
            .Skip((count * page) ?? 0)
            .Take(count ?? 50)
            .ToListAsync();
            return (value, totalItemCount);
        }

        public async Task<Teacher> addTeacher(Teacher teacher)
        {
            teacher.Role = Role.Teacher;
            await _Context.Teachers.AddAsync(teacher);
            await _Context.SaveChangesAsync();
            return teacher;
        }

        ///<summary>
        ///Öğretmeni verilen id üzerinden bulur.
        ///</summary>
        public async Task<Teacher> getTeacherById(int teacherId, int schoolId)
        {
            return await _Context.Teachers.FirstAsync(th => th.Id == teacherId && th.SchoolId == schoolId);
        }

        ///<summary>
        ///Öğretmeni siler.
        ///</summary>
        public async Task deleteTeacher(Teacher teacher)
        {
            _Context.Teachers.Remove(teacher);
            await _Context.SaveChangesAsync();
        }

        public async Task<Teacher> updateTeacher(Teacher newTeacher)
        {
            Teacher teacher = await _Context.Teachers
            .FirstOrDefaultAsync(th => th.Id == newTeacher.Id && th.SchoolId == newTeacher.SchoolId);

            if (teacher != null)
            {
                UtilityMethods.UpdateProps<Teacher>(teacher, newTeacher);
            }

            _Context.Update(teacher);
            await _Context.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> checkUsername(string username, int schoolId)
        {
            return await _Context.Teachers.AnyAsync(t => t.SchoolId == schoolId && t.Username.ToLower() == username.ToLower());
        }
    }
};