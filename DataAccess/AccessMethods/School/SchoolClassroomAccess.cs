using System;
using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos;
using System.Globalization;
using Entities.Dtos.ClassroomDtos;

namespace DataAccess.AccessMethods
{
    public class SchoolClassroomAccess
    {
        public SchoolClassroomAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;

        public async Task<List<ClassroomListDto>> getClassroomsById(int schoolId)
        {
            return await _Context.Classrooms
            .Where(p => p.SchoolId == schoolId)
            .OrderBy(p => p.Id)
            .Select(p => new ClassroomListDto
            {
                ClassroomId = p.Id,
                ClassroomName = p.Name,
            })
            .ToListAsync();
        }
        public async Task<(List<ClassroomDto>, int)> getClassroomsByPage(int schoolId, int? page, int? count)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            int totalItemCount = await _Context.Classrooms
            .Where(a => a.SchoolId == schoolId).CountAsync();
            List<ClassroomDto> value = await _Context.Classrooms
            .Where(a => a.SchoolId == schoolId)
            .OrderBy(order => order.Id)
            .Select(cr => new ClassroomDto
            {
                Id = cr.Id,
                Name = cr.Name,
                CreatedAtDate = cr.CreatedAt.ToString("dd.MM.yyyy"),
                UpdatedAtDate = cr.UpdatedAt.ToString("dd.MM.yyyy"),
                UpdatedAtTime = cr.UpdatedAt.ToString("HH:mm"),
                UpdatedBy = cr.UpdatedBy
            })
            .Skip((count * page) ?? 0)
            .Take(count ?? 9999)
            .ToListAsync();
            return (value, totalItemCount);
        }
        public async Task<(List<ClassroomDto>, int)> getClassroomsByQuery(int schoolId, int? page, int? count, string query)
        {
            page = page - 1 < 0 ? 0 : page - 1;
            query = query.ToLower();
            int totalItemCount = await _Context.Classrooms
            .Where(a => a.SchoolId == schoolId &&
            (
                a.Name.ToLower().Contains(query)
            )).CountAsync();
            List<ClassroomDto> value = await _Context.Classrooms
            .Where(a => a.SchoolId == schoolId &&
            (
                a.Name.ToLower().Contains(query)
            ))
            .OrderBy(order => order.Id)
            .Select(cr => new ClassroomDto
            {
                Id = cr.Id,
                Name = cr.Name,
                CreatedAtDate = cr.CreatedAt.ToString("dd.MM.yyyy"),
                UpdatedAtDate = cr.UpdatedAt.ToString("dd.MM.yyyy"),
                UpdatedAtTime = cr.UpdatedAt.ToString("HH:mm"),
                UpdatedBy = cr.UpdatedBy
            })
            .Skip((count * page) ?? 0)
            .Take(count ?? 9999)
            .ToListAsync();
            return (value, totalItemCount);
        }
        public async Task<Classroom> getClassroomById(int classroomId, int schoolId)
        {
            return await _Context.Classrooms.FirstAsync(cl => cl.Id == classroomId && cl.SchoolId == schoolId);
        }

        public async Task<Classroom> addClassroom(Classroom classroom)
        {
            await _Context.Classrooms.AddAsync(classroom);
            await _Context.SaveChangesAsync();
            return classroom;
        }

        ///<summary>
        ///İliskili propertyler disindaki(Orn. ogrencinin sinifi, ogrencinin dersleri) degerleri gunceller
        ///</summary>
        public async Task updateClassroom(Classroom newClassroom)
        {
            Classroom classroom = await _Context.Classrooms
            .FirstOrDefaultAsync(a => a.Id == newClassroom.Id && a.SchoolId == newClassroom.SchoolId);
            if (classroom != null)
            {
                UtilityMethods.UpdateProps<Classroom>(classroom, newClassroom);
            }
            _Context.Update(classroom);
            await _Context.SaveChangesAsync();
        }

        ///<summary>
        ///Sınıfının silinmesi istenen School'ın Id'si gönderilmelidir(integer)
        ///</summary>
        public async Task deleteClassroom(Classroom classroom)
        {
            _Context.Classrooms.Remove(classroom);
            await _Context.SaveChangesAsync();
        }
    }
};
