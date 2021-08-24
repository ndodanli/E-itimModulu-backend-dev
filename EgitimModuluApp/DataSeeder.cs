using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Entities;
// using BC = BCrypt.Net.BCrypt;
// using Microsoft.EntityFrameworkCore;
using System.Data;
using Npgsql;

namespace EgitimModuluApp
{
    public class DataSeeder
    {
        public static void Seed(DataAccess.DataContext context)
        {
            try
            {
                if (!checkIfAnyDataExist(context))
                {
                    handleTypes(context);
                    GenerateSeedData seeds = new GenerateSeedData();
                    List<object> paymentData
                    = seeds.getPaymentData(),
                    schoolData = seeds.getSchoolData(),
                    teacherData = seeds.getTeacherData(),
                    classroomData = seeds.getClassroomData(),
                    studentData = seeds.getStudentData(),
                    examData = seeds.getExamData(),
                    lessonData = seeds.getLessonData(),
                    contentData = seeds.getContentData(),
                    mediaFileData = seeds.getMediaFileData(),
                    questionData = seeds.getQuestionData(),
                    studentExamData = seeds.getStudentExamData(),
                    gradeData = seeds.getGradeData(),
                    homeworkData = seeds.getHomeworkData(),
                    classroomLessonData = seeds.getClassroomLessonData(),
                    teacherClassroomData = seeds.getTeacherClassroomData(),
                    classroomContentData = seeds.getClassroomContentData();
                    Console.WriteLine("YUKLEME YAPILIYOR...");
                    context.BulkInsert(paymentData);
                    context.BulkInsert(schoolData);
                    context.BulkInsert(teacherData);
                    context.BulkInsert(classroomData);
                    context.BulkInsert(studentData);
                    context.BulkInsert(lessonData);
                    context.BulkInsert(examData);
                    context.BulkInsert(contentData);
                    context.BulkInsert(mediaFileData);
                    context.BulkInsert(questionData);
                    context.BulkInsert(gradeData);
                    context.BulkInsert(homeworkData);
                    context.BulkInsert(studentExamData);
                    context.BulkInsert(classroomLessonData);
                    context.BulkInsert(teacherClassroomData);
                    context.BulkInsert(classroomContentData);
                    // context.SaveChanges();
                    handleSequence(context);
                    Console.WriteLine("YUKLEME TAMAMLANDI");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        private static void handleSequence(DataAccess.DataContext context)
        {

            using (var conn = new NpgsqlConnection("User ID=postgres;Password=denemE1@;Server=localhost;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;"))
            {
                var command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "SELECT setval('\"Classrooms_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Classrooms\"))UNION ALL SELECT setval('\"Exams_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Exams\"))UNION ALL SELECT setval('\"Grades_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Grades\"))UNION ALL SELECT setval('\"Lessons_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Lessons\"))UNION ALL SELECT setval('\"Payments_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Payments\"))UNION ALL SELECT setval('\"Questions_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Questions\"))UNION ALL SELECT setval('\"Schools_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Schools\"))UNION ALL SELECT setval('\"Students_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Students\"))UNION ALL SELECT setval('\"Teachers_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Teachers\"))UNION ALL SELECT setval('\"Contents_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Contents\"))UNION ALL SELECT setval('\"MediaFiles_Id_seq\"', (SELECT MAX(\"Id\") FROM \"MediaFiles\"))UNION ALL SELECT setval('\"RefreshTokens_Id_seq\"', (SELECT MAX(\"Id\") FROM \"RefreshTokens\"))UNION ALL SELECT setval('\"Homeworks_Id_seq\"', (SELECT MAX(\"Id\") FROM \"Homeworks\"))";
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                    }
                }
                conn.Close();
            }
        }
        private static void handleTypes(DataAccess.DataContext context)
        {

            using (var conn = new NpgsqlConnection("User ID=postgres;Password=denemE1@;Server=localhost;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;"))
            {
                conn.Open();
                conn.ReloadTypes();
                conn.Close();
            }
        }
        private static bool checkIfAnyDataExist(DataAccess.DataContext context)
        {
            return context.Schools.Any();
        }
        private class GenerateSeedData
        {

            public static List<object> classSeed, schoolSeed, examSeed, gradeSeed, paymentSeed,
            teacherSeed, lessonSeed, questionSeed, homeworkSeed,
            teacherClassSeed, classroomLessonSeed, classroomContentSeed, contentSeed, studentSeed, studentExamSeed;
            public static List<SchoolTemplate> schools;
            private int schoolLimit, paymentLimit;
            private string[] paymentMethods = { "standard", "medium", "pro" };
            private Random random;
            public GenerateSeedData()
            {

                int coefficient = 1;

                switch (DatabaseConfig.dbType)
                {
                    case DatabaseConfig.DatabaseType.Large:
                        schoolLimit = 20;
                        break;
                    case DatabaseConfig.DatabaseType.Medium:
                        schoolLimit = 15;
                        break;
                    case DatabaseConfig.DatabaseType.Small:
                        schoolLimit = 10;
                        break;
                    case DatabaseConfig.DatabaseType.Special:
                        schoolLimit = DatabaseConfig.schoolLimit;
                        coefficient = DatabaseConfig.COEFFICIENT;
                        break;
                    default:
                        Console.WriteLine("Hatali Database tipi");
                        break;
                }


                paymentLimit = 3;
                random = new Random();
                schools = new List<SchoolTemplate>();
                for (int i = 0; i < schoolLimit; i++)
                {
                    int _teacherLimit = random.Next(20, 26) * coefficient,
                    _classroomLimit = random.Next(30, 62) * coefficient,
                    _studentLimit = _classroomLimit * random.Next(10, 31) * coefficient,
                    _examLimit = _studentLimit * random.Next(0, 11) * coefficient,
                    _gradeLimit = _studentLimit * _examLimit,
                    _questionLimit = _examLimit * random.Next(10, 15) * coefficient,
                    _lessonLimit = _teacherLimit * random.Next(1, 4) * coefficient,
                    _homeworkLimit = _teacherLimit * random.Next(1, 3) * coefficient,
                    _studentExamLimit = _studentLimit * _examLimit,
                    _teacherClassroomLimit = _teacherLimit * _classroomLimit,
                    _classroomLessonLimit = _classroomLimit * _lessonLimit,
                    _contentLimit = _lessonLimit * random.Next(1, 26) * coefficient,
                    _mediaFileLimit = _contentLimit * random.Next(1, 10) * coefficient;
                    schools.Add(new SchoolTemplate
                    {
                        Id = i + 1,
                        teacherLimit = _teacherLimit,
                        studentLimit = _studentLimit,
                        classroomLimit = _classroomLimit,
                        examLimit = _examLimit,
                        gradeLimit = _gradeLimit,
                        questionLimit = _questionLimit,
                        lessonLimit = _lessonLimit,
                        contentLimit = _contentLimit,
                        homeworkLimit = _homeworkLimit,
                        studentExamLimit = _studentExamLimit,
                        teacherClassroomLimit = _teacherClassroomLimit,
                        classroomLessonLimit = _classroomLessonLimit,
                        mediaFileLimit = _mediaFileLimit
                    });
                }
            }
            private string getRandomTel()
            {
                Random rnd = new Random();
                return "50" + rnd.Next(5, 8) + rnd.Next(1000000, 10000000);
            }
            public static string getMail()
            {
                Random rnd = new Random();
                string[] mails = { "gmail", "hotmail", "outlook" };

                return mails[rnd.Next(0, 3)];
            }
            public static DateTime? randomBirthDate(DateTime from, DateTime to)
            {
                Random rnd = new Random();
                var range = to - from;

                var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

                return from + randTimeSpan;
            }
            private DateTime[] randomDate(string type)
            {
                random = new Random();
                int[] minutes = { 15, 30, 45 };
                int _startHour = random.Next(8, 21), _startMinute = minutes[random.Next(0, minutes.Length)];
                DateTime _startDate = new DateTime(2020, 10, 10, _startHour, _startMinute, 0);
                DateTime _dueDate = new DateTime(2020, 10, 17, _startHour, _startMinute, 0);
                if (type.Equals("day"))
                {
                    int range = (_dueDate - _startDate).Days;
                    _startDate = _startDate.AddDays(random.Next(0, range + 1));
                    _dueDate = _startDate.AddDays(random.Next(0, 2));
                    if (_startDate == _dueDate)
                    {
                        _dueDate = _dueDate.AddHours(random.Next(1, 3));
                        _dueDate = _dueDate.AddMinutes(minutes[random.Next(0, minutes.Length)]);
                    }
                }
                else
                {
                    int[] months = { 1, 6, 12 };
                    _dueDate = _dueDate.AddMonths(months[random.Next(0, months.Length)]);
                }

                DateTime[] dates = { _startDate, _dueDate };
                return dates;
            }
            private string getRandomImage()
            {
                Random rnd = new Random();
                string[] imgPaths = {
                    "256_daniel-gaffey-1060698-unsplash.jpg","256_jeremy-banks-798787-unsplash.jpg",
                    "256_joao-silas-636453-unsplash.jpg","256_luke-porter-261779-unsplash.jpg",
                    "256_michael-dam-258165-unsplash.jpg","256_rsz_1andy-lee-642320-unsplash.jpg",
                    "256_rsz_90-jiang-640827-unsplash.jpg","256_rsz_clem-onojeghuo-150467-unsplash.jpg",
                    "256_rsz_florian-perennes-594195-unsplash.jpg","256_rsz_james-gillespie-714755-unsplash.jpg",
                    "256_rsz_karl-s-973833-unsplash.jpg","256_rsz_nicolas-horn-689011-unsplash.jpg",
                    "256_rsz_sharina-mae-agellon-377466-unsplash.jpg","256_s-a-r-a-h-s-h-a-r-p-764291-unsplash.jpg",
                };
                return imgPaths[rnd.Next(0, 14)];
            }
            private string GetRandomString(int stringLength)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
                for (int i = 1; i <= numGuidsToConcat; i++)
                {
                    sb.Append(Guid.NewGuid().ToString("N"));
                }

                return sb.ToString(0, stringLength).ToUpper();
            }
            private TimeSpan randomTimeSpan()
            {
                random = new Random();
                int[] minutes = { 15, 30, 45 };
                return new TimeSpan(random.Next(0, 3), minutes[random.Next(0, 3)], 0);
            }

            public List<object> getSchoolData()
            {
                random = new Random();
                schoolSeed = new List<object>();
                for (int i = 1; i < schoolLimit + 1; i++)
                {
                    schoolSeed.Add(new School
                    {
                        Id = schools[i - 1].Id,
                        Username = "schoolUsername" + i,
                        Password = "schoolPassword" + i,
                        Tel = getRandomTel(),
                        Name = "schoolName" + i,
                        PaymentId = random.Next(1, paymentLimit + 1),
                        Role = Role.School,
                        EmailAddress = "schoolEmailAddress" + i + "@" + getMail() + ".com",
                        Code = GetRandomString(6)
                    });
                }
                Console.WriteLine("School data");
                return schoolSeed;
            }

            public List<object> getTeacherData()
            {
                random = new Random();
                teacherSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].teacherLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                    }
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        teacherSeed.Add(new Teacher
                        {
                            Id = j,
                            Username = "teacherUsername" + j,
                            Password = "teacherPassword" + j,
                            FirstName = "teacherFirstName" + j,
                            LastName = "teacherLastName" + j,
                            Tel = getRandomTel(),
                            EmailAddress = "teacherEmailAddress" + j + "@" + getMail() + ".com",
                            Role = Role.Teacher,
                            SchoolId = schools[i].Id,
                        });
                    }
                    Console.WriteLine("Teacher data");
                    _tempCount = schools[i].teacherLimit;
                }
                return teacherSeed;
            }
            public List<object> getHomeworkData()
            {
                random = new Random();
                homeworkSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _teacherdStart = 1;
                int _teacherIdRestart = 0;
                int _teacherIdEnd = 0;

                int _lessonIdStart = 1;
                int _lessonIdRestart = 0;
                int _lessonIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].homeworkLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _lessonIdStart = _lessonIdEnd + 1;
                        _teacherdStart = _teacherIdEnd + 1;
                    }
                    _lessonIdRestart = _lessonIdEnd;
                    _lessonIdEnd += schools[i].lessonLimit;
                    _teacherIdRestart = _teacherIdEnd;
                    _teacherIdEnd += schools[i].teacherLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        homeworkSeed.Add(new Homework
                        {
                            Id = j,
                            Name = "homeworkName" + j,
                            Subject = "homeworkSubject" + j,
                            Content = "homeworkContent" + j,
                            FilePath = "homeworkFilePath" + j,
                            Status = random.Next(0, 2) == 0 ? true : false,
                            TeacherId = _teacherdStart,
                            LessonId = _lessonIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_lessonIdStart >= _lessonIdEnd)
                        {
                            _lessonIdStart = _lessonIdRestart;
                        }
                        _lessonIdStart++;
                        if (_teacherdStart >= _teacherIdEnd)
                        {
                            _teacherdStart = _teacherIdRestart;
                        }
                        _teacherdStart++;
                    }
                    Console.WriteLine("Homework data");
                    _tempCount = schools[i].homeworkLimit;
                }
                return homeworkSeed;
            }
            public List<object> getClassroomData()
            {
                random = new Random();
                classSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].classroomLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                    }
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        classSeed.Add(new Classroom
                        {
                            Id = j,
                            Name = "classroomName" + j,
                            SchoolId = schools[i].Id,
                        });
                    }
                    Console.WriteLine("Classroom data");
                    _tempCount = schools[i].classroomLimit;
                }
                return classSeed;
            }
            public List<object> getStudentData()
            {
                random = new Random();
                studentSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _classroomIdStart = 1;
                int _classroomIdRestart = 0;
                int _classroomIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].studentLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _classroomIdStart = _classroomIdEnd + 1;
                    }
                    _classroomIdRestart = _classroomIdEnd;
                    _classroomIdEnd += schools[i].classroomLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        studentSeed.Add(new Student
                        {
                            Id = j,
                            Username = "studentUsername" + j,
                            Password = "studentPassword" + j,
                            FirstName = "studentFirstName" + j,
                            LastName = "studentLastName" + j,
                            Tel = getRandomTel(),
                            SchoolNumber = "studentSchoolNumber" + j,
                            EmailAddress = "studentEmailAddress" + j + "@" + getMail() + ".com",
                            BirthDate = randomBirthDate(new DateTime(2003, 01, 01), new DateTime(2015, 01, 01)),
                            Grade = random.Next(4, 13),
                            Role = Role.Student,
                            ClassroomId = _classroomIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_classroomIdStart >= _classroomIdEnd)
                        {
                            _classroomIdStart = _classroomIdRestart;
                        }
                        _classroomIdStart++;
                    }
                    Console.WriteLine("Student data");
                    _tempCount = schools[i].studentLimit;
                }
                return studentSeed;
            }

            public List<object> getExamData()
            {
                random = new Random();
                examSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _teacherIdStart = 1;
                int _teacherIdRestart = 0;
                int _teacherIdEnd = 0;

                int _lessonIdStart = 1;
                int _lessonIdRestart = 0;
                int _lessonIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].examLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _teacherIdStart = _teacherIdEnd + 1;
                        _lessonIdStart = _lessonIdEnd + 1;
                    }
                    _teacherIdRestart = _teacherIdEnd;
                    _teacherIdEnd += schools[i].teacherLimit;
                    _lessonIdRestart = _lessonIdEnd;
                    _lessonIdEnd += schools[i].lessonLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        DateTime[] dates = randomDate("day");
                        TimeSpan _timeSpan = new TimeSpan((dates[1] - dates[0]).Hours, (dates[1] - dates[0]).Minutes, (dates[1] - dates[0]).Seconds);

                        examSeed.Add(new Exam
                        {
                            Id = j,
                            Name = "examName" + j,
                            StartDate = dates[0],
                            DueDate = dates[1],
                            TotalTime = _timeSpan,
                            TeacherId = _teacherIdStart,
                            LessonId = _lessonIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_teacherIdStart >= _teacherIdEnd)
                        {
                            _teacherIdStart = _teacherIdRestart;
                        }
                        _teacherIdStart++;
                        if (_lessonIdStart >= _lessonIdEnd)
                        {
                            _lessonIdStart = _lessonIdRestart;
                        }
                        _lessonIdStart++;
                    }
                    Console.WriteLine("Exam data");
                    _tempCount = schools[i].examLimit;
                }
                return examSeed;
            }
            public List<object> getGradeData()
            {
                random = new Random();
                gradeSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _studentIdStart = 1;
                int _studentIdRestart = 0;
                int _studentIdEnd = 0;

                int _examIdStart = 1;
                int _examIdRestart = 0;
                int _examIdEnd = 0;

                // int reduceId = 0;
                // bool isStudentHasGrade = false;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].examLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _studentIdStart = _studentIdEnd + 1;
                        _examIdStart = _examIdEnd + 1;
                    }
                    _studentIdRestart = _studentIdEnd;
                    _studentIdEnd += schools[i].studentLimit;
                    _examIdRestart = _examIdEnd;
                    _examIdEnd += schools[i].examLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        // isStudentHasGrade = random.Next(0, 2) == 1 ? isStudentHasGrade = true : false;

                        // if (isStudentHasGrade)
                        // {
                        gradeSeed.Add(new Grade
                        {
                            // Id = (j + 1 - reduceId),
                            Id = (j + 1),
                            Score = random.Next(0, 101),
                            // Description = "gradeDescription" + (j + 1 - reduceId),
                            Description = "gradeDescription" + (j + 1),
                            StudentId = _studentIdStart,
                            ExamId = _examIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_studentIdStart >= _studentIdEnd)
                        {
                            _studentIdStart = _studentIdRestart;
                        }
                        _studentIdStart++;
                        if (_examIdStart >= _examIdEnd)
                        {
                            _examIdStart = _examIdRestart;
                        }
                        _examIdStart++;
                        // }
                        // else
                        // {
                        //     reduceId++;
                        // }
                    }
                    Console.WriteLine("Grade data");
                    _tempCount = schools[i].gradeLimit;
                }
                return gradeSeed;
            }
            public List<object> getLessonData()
            {
                random = new Random();
                lessonSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _teacherIdStart = 1;
                int _teacherIdRestart = 0;
                int _teacherIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].lessonLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _teacherIdStart = _teacherIdEnd + 1;
                    }
                    _teacherIdRestart = _teacherIdEnd;
                    _teacherIdEnd += schools[i].teacherLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        lessonSeed.Add(new Lesson
                        {
                            Id = j,
                            Name = "lessonName" + j,
                            LessonCode = GetRandomString(6),
                            TeacherId = _teacherIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_teacherIdStart >= _teacherIdEnd)
                        {
                            _teacherIdStart = _teacherIdRestart;
                        }
                        _teacherIdStart++;
                    }
                    Console.WriteLine("Lesson data");
                    _tempCount = schools[i].lessonLimit;
                }
                return lessonSeed;
            }
            public List<object> getContentData()
            {
                random = new Random();
                contentSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _lessonIdStart = 1;
                int _lessonIdRestart = 0;
                int _lessonIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].contentLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _lessonIdStart = _lessonIdEnd + 1;
                    }
                    _lessonIdRestart = _lessonIdEnd;
                    _lessonIdEnd += schools[i].lessonLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        contentSeed.Add(new Content
                        {
                            Id = j,
                            Title = "contentTitle" + j,
                            Description = "contentDescription" + j,
                            Summary = "contentSummary" + j,
                            Status = random.Next(0, 2) == 0 ? false : true,
                            LessonId = _lessonIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_lessonIdStart >= _lessonIdEnd)
                        {
                            _lessonIdStart = _lessonIdRestart;
                        }
                        _lessonIdStart++;
                    }
                    Console.WriteLine("Content data");
                    _tempCount = schools[i].contentLimit;
                }
                return contentSeed;
            }
            public List<object> getMediaFileData()
            {
                random = new Random();
                contentSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _contentIdStart = 1;
                int _contentIdRestart = 0;
                int _contentIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].mediaFileLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _contentIdStart = _contentIdEnd + 1;
                    }
                    _contentIdRestart = _contentIdEnd;
                    _contentIdEnd += schools[i].contentLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        contentSeed.Add(new MediaFile
                        {
                            Id = j,
                            Path = "assets/images/" + getRandomImage(),
                            Status = random.Next(0, 2) == 0 ? false : true,
                            ContentId = _contentIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_contentIdStart >= _contentIdEnd)
                        {
                            _contentIdStart = _contentIdRestart;
                        }
                        _contentIdStart++;
                    }
                    Console.WriteLine("MediaFile data");
                    _tempCount = schools[i].mediaFileLimit;
                }
                return contentSeed;
            }
            public List<object> getPaymentData()
            {
                random = new Random();
                paymentSeed = new List<object>();
                for (int i = 1; i < paymentLimit + 1; i++)
                {
                    DateTime[] dates = randomDate("month");
                    paymentSeed.Add(new Payment
                    {
                        Id = i,
                        PaymentMethod = paymentMethods[random.Next(1, paymentMethods.Length)],
                        MaxStudent = schools[i - 1].studentLimit,
                        MaxTeacher = schools[i - 1].teacherLimit,
                        StartDate = dates[0],
                        DueDate = dates[1],

                    });
                }
                Console.WriteLine("Payment data");
                return paymentSeed;
            }
            public List<object> getQuestionData()
            {
                random = new Random();
                questionSeed = new List<object>();
                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _examIdStart = 1;
                int _examIdRestart = 0;
                int _examIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].questionLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _examIdStart = _examIdEnd + 1;
                    }
                    _examIdRestart = _examIdEnd;
                    _examIdEnd += schools[i].examLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        questionSeed.Add(new Question
                        {
                            Id = j,
                            ExamQuestion = "examQuestion" + j,
                            Answer = "examAnswer" + j,
                            ExamId = _examIdStart,
                            SchoolId = schools[i].Id,
                        });
                        if (_examIdStart >= _examIdEnd)
                        {
                            _examIdStart = _examIdRestart;
                        }
                        _examIdStart++;
                    }
                    Console.WriteLine("Question data");
                    _tempCount = schools[i].questionLimit;
                }
                return questionSeed;
            }
            public List<object> getStudentExamData()
            {
                random = new Random();
                studentExamSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0; //

                int _examIdStart = 1;
                int _examIdRestart = 0;
                int _examIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].studentLimit; //
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _examIdStart = _examIdEnd + 1;
                    }
                    _examIdRestart = _examIdEnd;
                    _examIdEnd += schools[i].examLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        studentExamSeed.Add(new StudentExam
                        {
                            StudentId = j,
                            ExamId = _examIdStart //
                        });

                        if (_examIdStart >= _examIdEnd)
                        {
                            _examIdStart = _examIdRestart; //
                        }
                        _examIdStart++;
                    }
                    Console.WriteLine("Student data");
                    _tempCount = schools[i].studentLimit;
                }
                return studentExamSeed;
            }

            public List<object> getClassroomLessonData()
            {
                random = new Random();
                classroomLessonSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _classroomIdStart = 1;
                int _classroomIdRestart = 0;
                int _classroomIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].lessonLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _classroomIdStart = _classroomIdEnd + 1;
                    }
                    _classroomIdRestart = _classroomIdEnd;
                    _classroomIdEnd += schools[i].classroomLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        classroomLessonSeed.Add(new ClassroomLesson
                        {
                            ClassroomId = _classroomIdStart,
                            LessonId = j
                        });

                        if (_classroomIdStart >= _classroomIdEnd)
                        {
                            _classroomIdStart = _classroomIdRestart;
                        }
                        _classroomIdStart++;

                    }
                    Console.WriteLine("Classroomclassroom data");
                    _tempCount = schools[i].lessonLimit;
                }
                return classroomLessonSeed;
            }

            public List<object> getTeacherClassroomData()
            {
                random = new Random();
                teacherClassSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _teacherIdStart = 1;
                int _teacherIdRestart = 0;
                int _teacherIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].classroomLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _teacherIdStart = _teacherIdEnd + 1;
                    }
                    _teacherIdRestart = _teacherIdEnd;
                    _teacherIdEnd += schools[i].teacherLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        teacherClassSeed.Add(new TeacherClassroom
                        {
                            TeacherId = _teacherIdStart,
                            ClassroomId = j
                        });
                        if (_teacherIdStart >= _teacherIdEnd)
                        {
                            _teacherIdStart = _teacherIdRestart;
                        }
                        _teacherIdStart++;
                    }
                    Console.WriteLine("TeacherClass data");
                    _tempCount = schools[i].classroomLimit;
                }
                return teacherClassSeed;
            }

            public List<object> getClassroomContentData()
            {
                random = new Random();
                classroomContentSeed = new List<object>();

                int _countStart = 1;
                int _countEnd = 1;
                int _tempCount = 0;

                int _classroomIdStart = 1;
                int _classroomIdRestart = 0;
                int _classroomIdEnd = 0;
                for (int i = 0; i < schoolLimit; i++)
                {
                    _countEnd += schools[i].contentLimit;
                    if (i != 0)
                    {
                        _countStart += _tempCount;
                        _classroomIdStart = _classroomIdEnd + 1;
                    }
                    _classroomIdRestart = _classroomIdEnd;
                    _classroomIdEnd += schools[i].classroomLimit;
                    for (int j = _countStart; j < _countEnd; j++)
                    {
                        classroomContentSeed.Add(new ClassroomContent
                        {
                            ClassroomId = _classroomIdStart,
                            ContentId = j
                        });
                        if (_classroomIdStart >= _classroomIdEnd)
                        {
                            _classroomIdStart = _classroomIdRestart;
                        }
                        _classroomIdStart++;
                    }
                    Console.WriteLine("classroomClass data");
                    _tempCount = schools[i].contentLimit;
                }
                return classroomContentSeed;
            }
        }
        public class SchoolTemplate : IEnumerable
        {
            public int Id;
            public int teacherLimit;
            public int classroomLimit;
            public int examLimit;
            public int questionLimit;
            public int lessonLimit;
            public int gradeLimit;
            public int paymentLimit;
            public int studentLimit;
            public int contentLimit;
            public int homeworkLimit;
            public int mediaFileLimit;
            public int studentExamLimit;
            public int teacherClassroomLimit;
            public int teacherLessonLimit;
            public int classroomLessonLimit;
            private List<SchoolTemplate> items = new List<SchoolTemplate>();
            public IEnumerator GetEnumerator()
            {
                return this.items.GetEnumerator();
            }
        }
    }
}