using DataAccess;
using DataAccess.AccessMethods;

namespace EgitimModuluApp.DataAccessLayer
{
    public interface IDataAccessContext
    {
        ClassroomAccess ClassroomAccess();
        ExamAccess ExamAccess();
        GradeAccess GradeAccess();
        LessonAccess LessonAccess();
        PaymentAccess PaymentAccess();
        QuestionAccess QuestionAccess();
        SchoolAccess SchoolAccess();
        StudentAccess StudentAccess();
        TeacherAccess TeacherAccess();
        SchoolStudentAccess SchoolStudentAccess();
        SchoolClassroomAccess SchoolClassroomAccess();
        SchoolTeacherAccess SchoolTeacherAccess();
        SchoolLessonAccess SchoolLessonAccess();
        TeacherLessonAccess TeacherLessonAccess();
        TeacherExamAccess TeacherExamAccess();
        TeacherHomeworkAccess TeacherHomeworkAccess();
    }
    public class DataAccessContext : IDataAccessContext
    {
        public DataContext _dataBase;
        public DataAccessContext(DataContext context)
        {
            _dataBase = context;
        }

        public ClassroomAccess ClassroomAccess() { return new ClassroomAccess(_dataBase); }
        public ExamAccess ExamAccess() { return new ExamAccess(_dataBase); }
        public GradeAccess GradeAccess() { return new GradeAccess(_dataBase); }
        public LessonAccess LessonAccess() { return new LessonAccess(_dataBase); }
        public PaymentAccess PaymentAccess() { return new PaymentAccess(_dataBase); }
        public QuestionAccess QuestionAccess() { return new QuestionAccess(_dataBase); }
        public SchoolAccess SchoolAccess() { return new SchoolAccess(_dataBase); }
        public StudentAccess StudentAccess() { return new StudentAccess(_dataBase); }
        public TeacherAccess TeacherAccess() { return new TeacherAccess(_dataBase); }
        public SchoolStudentAccess SchoolStudentAccess() { return new SchoolStudentAccess(_dataBase); }
        public SchoolClassroomAccess SchoolClassroomAccess() { return new SchoolClassroomAccess(_dataBase); }
        public SchoolTeacherAccess SchoolTeacherAccess() { return new SchoolTeacherAccess(_dataBase); }
        public SchoolLessonAccess SchoolLessonAccess() { return new SchoolLessonAccess(_dataBase); }
        public TeacherLessonAccess TeacherLessonAccess() { return new TeacherLessonAccess(_dataBase); }
        public TeacherExamAccess TeacherExamAccess() { return new TeacherExamAccess(_dataBase); }
        public TeacherHomeworkAccess TeacherHomeworkAccess() { return new TeacherHomeworkAccess(_dataBase); }
    }
}