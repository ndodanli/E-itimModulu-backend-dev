namespace DataAccess.AccessMethods
{
    public class ClassroomAccess
    {
        public ClassroomAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;
    }
};
