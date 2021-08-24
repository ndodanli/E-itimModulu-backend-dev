namespace DataAccess.AccessMethods
{
    public class GradeAccess
    {
        public GradeAccess(DataContext context)
        {
            _Context = context;
        }
        private DataContext _Context;
    }
};
