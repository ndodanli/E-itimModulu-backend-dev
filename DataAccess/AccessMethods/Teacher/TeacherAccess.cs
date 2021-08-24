using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.AccessMethods
{
    public class TeacherAccess
    {
        private DataContext _Context;
        public TeacherAccess(DataContext context)
        {
            _Context = context;
        }
        
        public async Task<Teacher> updateTeacher(Teacher newTeacher)
        {
            Teacher teacher = await _Context.Teachers
            .FirstOrDefaultAsync(a => a.Id == newTeacher.Id);
            if (teacher != null)
            {
                UtilityMethods.UpdateProps<Teacher>(teacher, newTeacher);
            }
            _Context.Update(teacher);
            await _Context.SaveChangesAsync();
            return teacher;
        }
    }
};