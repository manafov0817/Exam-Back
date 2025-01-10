namespace ExamApp.Business.Models
{
    public record ExamDTO(string LessonCode,
                          int StudentNumber,
                          DateTime ExamDate,
                          int Grade);
}