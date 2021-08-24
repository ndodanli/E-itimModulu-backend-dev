using System;
using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities.Dtos.LessonDtos;
using System.Collections.Generic;

namespace DataAccess.AccessMethods
{
    public class LessonAccess
    {
        private DataContext _Context;
        public LessonAccess(DataContext context)
        {
            _Context = context;
        }
    }
};
