namespace DataAccess.AccessMethods
{
    public class StudentAccess
    {
        private DataContext _Context;
        public StudentAccess(DataContext context)
        {
            _Context = context;
        }

    }
};