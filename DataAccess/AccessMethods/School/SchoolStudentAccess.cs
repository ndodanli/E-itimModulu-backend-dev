using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Dtos;
using Entities.Dtos.StudentDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.AccessMethods
{
    public class SchoolStudentAccess
    {
        private DataContext _Context;
        public SchoolStudentAccess(DataContext context)
        {
            _Context = context;
        }
        public async Task<(List<StudentListDto>, int)> getStudentsByPage(int? schoolId, int? page, int? count)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            int totalItemCount = await _Context.Students
            .Where(a => a.SchoolId == schoolId).CountAsync();
            List<StudentListDto> value = await _Context.Students
            .Where(p => p.SchoolId == schoolId)
            .OrderBy(p => p.Id)
            .Select(p => new StudentListDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Username = p.Username,
                Tel = p.Tel,
                EmailAddress = p.EmailAddress,
                BirthDate = p.BirthDate.HasValue ? p.BirthDate.Value.ToString("yyyy-MM-dd") : null,
                SchoolNumber = p.SchoolNumber,
                CreatedAtDate = p.CreatedAt.ToString("dd.MM.yy"),
                UpdatedAtDate = p.UpdatedAt.ToString("dd.MM.yy"),
                UpdatedAtTime = p.UpdatedAt.ToString("HH:mm"),
                SchoolId = p.SchoolId,
                ClassroomId = p.ClassroomId,
            })
            .Skip((count * page) ?? 0)
            .Take(count ?? 50)
            .ToListAsync();
            return (value, totalItemCount);
        }
        public async Task<(List<StudentListDto>, int)> getStudentsByQuery(int? schoolId, int? page, int? count, string query)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            query = query.ToLower();
            int totalItemCount = await _Context.Students
            .Where(p => p.SchoolId == schoolId &&
            (
                p.SchoolNumber.Contains(query) ||
                p.FirstName.ToLower().Contains(query) ||
                p.LastName.ToLower().Contains(query) ||
                p.Username.ToLower().Contains(query) ||
                p.Tel.Contains(query) ||
                p.EmailAddress.ToLower().Contains(query)
            )).CountAsync();
            List<StudentListDto> value = await _Context.Students
                            .Where(p => p.SchoolId == schoolId &&
                             (
                                p.SchoolNumber.Contains(query) ||
                                p.FirstName.ToLower().Contains(query) ||
                                p.LastName.ToLower().Contains(query) ||
                                p.Username.ToLower().Contains(query) ||
                                p.Tel.Contains(query) ||
                                p.EmailAddress.ToLower().Contains(query)
                             ))
                            .OrderBy(s => s.Id)
                            .Select(s => new StudentListDto
                            {
                                Id = s.Id,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Username = s.Username,
                                Tel = s.Tel,
                                EmailAddress = s.EmailAddress,
                                BirthDate = s.BirthDate.HasValue ? s.BirthDate.Value.ToString("yyyy-MM-dd") : null,
                                SchoolNumber = s.SchoolNumber,
                                CreatedAtDate = s.CreatedAt.ToString("dd.MM.yy"),
                                UpdatedAtDate = s.UpdatedAt.ToString("dd.MM.yy"),
                                UpdatedAtTime = s.UpdatedAt.ToString("HH:mm"),
                                SchoolId = s.SchoolId,
                                ClassroomId = s.ClassroomId,
                            })
                            .Skip((count * page) ?? 0)
                            .Take(count ?? 50)
                          .ToListAsync();
            return (value, totalItemCount);
        }

        ///<summary>
        ///Öğrenciyi verilen id üzerinden bulur.
        ///</summary>
        public async Task<Student> getStudentById(int studentId, int schoolId)
        {
            return await _Context.Students
            .FirstAsync(std => std.Id == studentId && std.SchoolId == schoolId); //FirstOrDefault() kullanılır ise null döner ve delete için tekrar işlem
                                                                                 //yapılması gerekir, bunu önlemek için First() kullanıldı(bulunmaz ise hata fırlat).
        }

        ///<summary>
        ///Yeni öğrenci ekler.
        ///</summary>
        public async Task<Student> addStudent(Student student)
        {
            student.Role = Role.Student;
            await _Context.Students.AddAsync(student);
            await _Context.SaveChangesAsync();
            return student;
        }

        ///<summary>
        ///Öğrencinin kişisel ve hesap bilgilerini günceller.
        ///</summary>
        public async Task<Student> updateStudent(Student newStudent)
        {
            Student student = await _Context.Students.FirstOrDefaultAsync(a => a.Id == newStudent.Id && a.SchoolId == newStudent.SchoolId);

            if (student != null)
            {
                UtilityMethods.UpdateProps<Student>(student, newStudent);
            }

            _Context.Update(student);
            await _Context.SaveChangesAsync();
            return student;
        }

        ///<summary>
        ///Öğrenciyi siler.
        ///</summary>
        public async Task deleteStudent(Student student)
        {
            _Context.Students.Remove(student);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> checkUsername(string username, int schoolId)
        {
            return await _Context.Students.AnyAsync(t => t.SchoolId == schoolId && t.Username.ToLower() == username.ToLower());
        }
    }
}