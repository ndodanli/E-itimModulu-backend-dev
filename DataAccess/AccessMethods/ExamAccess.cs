using System;
using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.AccessMethods
{
    public class ExamAccess
    {
        public ExamAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;
    }
};
