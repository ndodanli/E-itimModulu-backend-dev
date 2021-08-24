using Entities;
using Microsoft.EntityFrameworkCore;
public class BuildModel
{
    public static void SetRelationships(ModelBuilder modelBuilder)
    {
        #region Time Fields
        //CreatedAt UpdatedAt School
        modelBuilder.Entity<School>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<School>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<School>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Teacher
        modelBuilder.Entity<Teacher>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Teacher>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Teacher>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Student
        modelBuilder.Entity<Student>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Student>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Student>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt ClassRoom
        modelBuilder.Entity<Classroom>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Classroom>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Classroom>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Exam
        modelBuilder.Entity<Exam>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Exam>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Exam>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Grade
        modelBuilder.Entity<Grade>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Grade>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Grade>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Lesson
        modelBuilder.Entity<Lesson>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Lesson>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Lesson>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Payment
        modelBuilder.Entity<Payment>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Payment>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Payment>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Question
        modelBuilder.Entity<Question>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Question>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Question>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Content
        modelBuilder.Entity<Content>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Content>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Content>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt MediaFile
        modelBuilder.Entity<MediaFile>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<MediaFile>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<MediaFile>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt Homework
        modelBuilder.Entity<Homework>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<Homework>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Homework>()
            .Property(p => p.UpdatedBy);

        //CreatedAt UpdatedAt RefreshToken
        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<RefreshToken>()
            .Property(p => p.UpdatedAt);

        //CreatedAt UpdatedAt HomeworkStudent
        modelBuilder.Entity<HomeworkStudent>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<HomeworkStudent>()
            .Property(p => p.UpdatedAt);

        //CreatedAt UpdatedAt HomeworkClassroom
        modelBuilder.Entity<HomeworkClassroom>()
            .Property(p => p.CreatedAt);

        modelBuilder.Entity<HomeworkClassroom>()
            .Property(p => p.UpdatedAt);

        #endregion

        #region Delete Behaviors
        //School relations
        modelBuilder.Entity<School>()
        .HasMany(a => a.Grades)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Lessons)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Teachers)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Classrooms)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Students)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Exams)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasOne(a => a.RefreshToken)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Contents)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.MediaFiles)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<School>()
        .HasMany(a => a.Questions)
        .WithOne(b => b.School)
        .OnDelete(DeleteBehavior.Cascade);

        //Lesson relations
        modelBuilder.Entity<Lesson>()
        .HasMany(a => a.Contents)
        .WithOne(b => b.Lesson)
        .OnDelete(DeleteBehavior.Cascade);

        //Content relations
        modelBuilder.Entity<Content>()
        .HasMany(a => a.MediaFiles)
        .WithOne(b => b.Content)
        .OnDelete(DeleteBehavior.Cascade);

        //Teacher relations
        modelBuilder.Entity<Teacher>()
        .HasMany(a => a.Exams)
        .WithOne(b => b.Teacher)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Teacher>()
        .HasMany(a => a.Lessons)
        .WithOne(b => b.Teacher)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Teacher>()
        .HasOne(a => a.RefreshToken)
        .WithOne(b => b.Teacher)
        .OnDelete(DeleteBehavior.Cascade);

        //Classroom relations
        modelBuilder.Entity<Classroom>()
        .HasMany(a => a.Students)
        .WithOne(b => b.Classroom)
        .OnDelete(DeleteBehavior.SetNull);

        //Student
        modelBuilder.Entity<Student>()
        .HasMany(a => a.Grades)
        .WithOne(b => b.Student)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Student>()
        .HasOne(a => a.RefreshToken)
        .WithOne(b => b.Student)
        .OnDelete(DeleteBehavior.Cascade);

        //Exam relations
        modelBuilder.Entity<Exam>()
        .HasMany(a => a.Grades)
        .WithOne(b => b.Exam)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Exam>()
        .HasMany(a => a.Questions)
        .WithOne(b => b.Exam)
        .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region Properties

        #region Payment
        modelBuilder.Entity<Payment>()
        .HasKey(k => k.Id);
        modelBuilder.Entity<Payment>()
        .Property(p => p.MaxStudent)
        .IsRequired();
        modelBuilder.Entity<Payment>()
        .Property(p => p.MaxTeacher)
        .IsRequired();
        modelBuilder.Entity<Payment>()
        .Property(p => p.StartDate)
        .IsRequired();
        modelBuilder.Entity<Payment>()
        .Property(p => p.DueDate)
        .IsRequired();
        #endregion

        #region School
        modelBuilder.Entity<School>()
        .HasKey(k => k.Id);
        modelBuilder.Entity<School>()
        .Property(p => p.Username)
        .IsRequired()
        .HasMaxLength(50);
        modelBuilder.Entity<School>()
        .Property(p => p.Password)
        .IsRequired()
        .HasMaxLength(250);
        modelBuilder.Entity<School>()
        .Property(p => p.Name)
        .IsRequired()
        .HasMaxLength(50);
        modelBuilder.Entity<School>()
        .Property(p => p.Tel)
        .IsRequired()
        .HasMaxLength(20);
        modelBuilder.Entity<School>()
        .Property(p => p.EmailAddress)
        .IsRequired()
        .HasMaxLength(150);
        modelBuilder.Entity<School>()
        .Property(p => p.Code)
        .HasMaxLength(6)
        .IsRequired();
        modelBuilder.Entity<School>()
        .Property(p => p.Role)
        .IsRequired();
        modelBuilder.Entity<School>()
        .Property(p => p.PaymentId)
        .IsRequired();
        #endregion

        #region Teacher
        modelBuilder.Entity<Teacher>()
        .HasKey(k => k.Id);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.Username)
        .IsRequired()
        .HasMaxLength(50);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.Password)
        .IsRequired()
        .HasMaxLength(250);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.FirstName)
        .HasMaxLength(50);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.LastName)
        .HasMaxLength(50);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.Tel)
        .HasMaxLength(20);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.EmailAddress)
        .HasMaxLength(150);
        modelBuilder.Entity<Teacher>()
        .Property(p => p.Role)
        .IsRequired();
        modelBuilder.Entity<Teacher>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Student
        modelBuilder.Entity<Student>()
        .HasKey(k => k.Id);
        modelBuilder.Entity<Student>()
        .Property(p => p.Username)
        .IsRequired()
        .HasMaxLength(50);
        modelBuilder.Entity<Student>()
        .Property(p => p.Password)
        .IsRequired()
        .HasMaxLength(250);
        modelBuilder.Entity<Student>()
        .Property(p => p.FirstName)
        .HasMaxLength(50);
        modelBuilder.Entity<Student>()
        .Property(p => p.LastName)
        .HasMaxLength(50);
        modelBuilder.Entity<Student>()
        .Property(p => p.Tel)
        .HasMaxLength(20);
        modelBuilder.Entity<Student>()
        .Property(p => p.SchoolNumber)
        .HasMaxLength(50);
        modelBuilder.Entity<Student>()
        .Property(p => p.EmailAddress)
        .HasMaxLength(150);
        modelBuilder.Entity<Student>()
        .Property(p => p.Role)
        .IsRequired();
        modelBuilder.Entity<Student>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region RefreshToken
        modelBuilder.Entity<RefreshToken>()
        .Property(p => p.Token)
        .IsRequired();
        modelBuilder.Entity<RefreshToken>()
        .Property(p => p.Expires)
        .IsRequired();
        modelBuilder.Entity<RefreshToken>()
        .Property(p => p.CreatedByIp)
        .IsRequired();
        #endregion

        #region Question
        modelBuilder.Entity<Question>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region MediaFile
        modelBuilder.Entity<MediaFile>()
        .Property(p => p.ContentId)
        .IsRequired();
        modelBuilder.Entity<MediaFile>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Lesson
        modelBuilder.Entity<Lesson>()
        .Property(p => p.Name)
        .IsRequired();
        modelBuilder.Entity<Lesson>()
        .Property(p => p.LessonCode)
        .IsRequired();
        modelBuilder.Entity<Lesson>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Grade
        modelBuilder.Entity<Grade>()
        .Property(p => p.ExamId)
        .IsRequired();
        modelBuilder.Entity<Grade>()
        .Property(p => p.StudentId)
        .IsRequired();
        modelBuilder.Entity<Grade>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Homework
        modelBuilder.Entity<Homework>()
        .Property(p => p.TeacherId)
        .IsRequired();
        modelBuilder.Entity<Homework>()
        .Property(p => p.LessonId)
        .IsRequired();
        modelBuilder.Entity<Homework>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Exam
        modelBuilder.Entity<Exam>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Content
        modelBuilder.Entity<Content>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        #region Classroom
        modelBuilder.Entity<Classroom>()
        .Property(p => p.Name)
        .IsRequired();
        modelBuilder.Entity<Classroom>()
        .Property(p => p.SchoolId)
        .IsRequired();
        #endregion

        //...güncellenecek
        #endregion

        #region Indexes

        modelBuilder.Entity<School>()
        .HasIndex(p => p.Code)
        .IsUnique();
        modelBuilder.Entity<RefreshToken>()
        .HasIndex(p => p.Token);    //unique olup olmaması değerlendirilecek. ...güncellenecek

        #endregion

        #region Many-to-many Relationships
        //Teacher-Classroom
        modelBuilder.Entity<Teacher>()
       .HasMany(x => x.Classrooms)
                   .WithMany(x => x.Teachers)
                   .UsingEntity<TeacherClassroom>(
                       x => x.HasOne(xs => xs.Classroom).WithMany().OnDelete(DeleteBehavior.Cascade),
                       x => x.HasOne(xs => xs.Teacher).WithMany().OnDelete(DeleteBehavior.Cascade))
                   .HasKey(x => new { x.TeacherId, x.ClassroomId });

        modelBuilder.Entity<Classroom>()

        //Lesson-Classroom
        .HasMany(x => x.Lessons)
                    .WithMany(x => x.Classrooms)
                    .UsingEntity<ClassroomLesson>(
                        x => x.HasOne(xs => xs.Lesson).WithMany().OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne(xs => xs.Classroom).WithMany().OnDelete(DeleteBehavior.Cascade))
                    .HasKey(x => new { x.LessonId, x.ClassroomId });

        modelBuilder.Entity<Classroom>()

        //Content-Classroom
        .HasMany(x => x.Contents)
                    .WithMany(x => x.Classrooms)
                    .UsingEntity<ClassroomContent>(
                        x => x.HasOne(xs => xs.Content).WithMany().OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne(xs => xs.Classroom).WithMany().OnDelete(DeleteBehavior.Cascade))
                    .HasKey(x => new { x.ContentId, x.ClassroomId });

        //Student-Exam
        modelBuilder.Entity<Student>()
        .HasMany(x => x.Exams)
                    .WithMany(x => x.Students)
                    .UsingEntity<StudentExam>(
                        x => x.HasOne(xs => xs.Exam).WithMany().OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne(xs => xs.Student).WithMany().OnDelete(DeleteBehavior.Cascade))
                    .HasKey(x => new { x.ExamId, x.StudentId });

        //Homework-Student
        modelBuilder.Entity<Homework>()
        .HasMany(x => x.Students)
                    .WithMany(x => x.Homeworks)
                    .UsingEntity<HomeworkStudent>(
                        x => x.HasOne(xs => xs.Student).WithMany().OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne(xs => xs.Homework).WithMany().OnDelete(DeleteBehavior.Cascade))
                    .HasKey(x => new { x.StudentId, x.HomeworkId });

        //Homework-Classroom
        modelBuilder.Entity<Homework>()
        .HasMany(x => x.Classrooms)
                    .WithMany(x => x.Homeworks)
                    .UsingEntity<HomeworkClassroom>(
                        x => x.HasOne(xs => xs.Classroom).WithMany().OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne(xs => xs.Homework).WithMany().OnDelete(DeleteBehavior.Cascade))
                    .HasKey(x => new { x.ClassroomId, x.HomeworkId });
        #endregion
    }
}
