using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamApp.Entities
{
    public class Exam : BaseEntity
    {
        [Column(TypeName = "char(3)")]
        [Required]
        [StringLength(3, ErrorMessage = "LessonCode cannot exceed 3 characters.")]
        public string LessonCode { get; set; }

        // Limiting StudentNumber to a maximum of 5 digits
        [Column(TypeName = "int")]
        [Required]
        [Range(0, 99999, ErrorMessage = "StudentNumber must be between 0 and 99999.")]

        public int StudentNumber { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime ExamDate { get; set; }

        // Limiting Grade to a single digit (0-9)
        [Column(TypeName = "int")]
        [Required]
        [Range(0, 9, ErrorMessage = "Grade must be between 0 and 9.")]
        public int Grade { get; set; }

        [ForeignKey("LessonCode")]
        public Lesson Lesson { get; set; }

        [ForeignKey("Number")]
        public Student Student { get; set; }
    }
}
