using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Dtos;
using Entities.Dtos.SchoolDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.AccessMethods
{
    public class SchoolAccess
    {
        private DataContext _Context;
        public SchoolAccess(DataContext context)
        {
            _Context = context;
        }
        public async Task<DashboardDto> getTotalCountByDashboard(int schoolId)
        {
            DashboardDto dashboardDto = new DashboardDto()
            {
                totalOnline = await _Context.Students.Where(s => s.SchoolId == schoolId).CountAsync(),
                totalStudent = await _Context.Students.Where(s => s.SchoolId == schoolId).CountAsync(),
                totalTeacher = await _Context.Teachers.Where(s => s.SchoolId == schoolId).CountAsync(),
                totalActiveClassroom = await _Context.Classrooms.Where(s => s.SchoolId == schoolId).CountAsync()
            };

            return dashboardDto;
        }
        public async Task<School> updateSchool(School newSchool)
        {
            School school = await _Context.Schools
            .FirstOrDefaultAsync(a => a.Id == newSchool.Id);
            if (school != null)
            {
                UtilityMethods.UpdateProps<School>(school, newSchool);
            }
            _Context.Update(school);
            await _Context.SaveChangesAsync();
            return school;
        }
    }
};