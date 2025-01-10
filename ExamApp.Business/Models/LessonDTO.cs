namespace ExamApp.Business.Models
{
    public record LessonDTO(string LessonCode,
                            string LessonName,
                            int Class,
                            string TeacherFirstName,
                            string TeacherLastName);
}
