using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Entities
{
    public class Lesson : BaseEntity
    {
        [Column(TypeName = "char(3)")]
        [Required]
        [StringLength(3, ErrorMessage = "LessonCode cannot exceed 3 characters.")]
        public string LessonCode { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        [StringLength(30, ErrorMessage = "LessonName cannot exceed 30 characters.")]
        public string LessonName { get; set; }

        // Limiting Class to be between 1 and 99 (int type)
        [Column(TypeName = "int")]
        [Required]
        [Range(1, 99, ErrorMessage = "Class must be between 1 and 99.")]
        public int Class { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        [StringLength(20, ErrorMessage = "TeacherFirstName cannot exceed 20 characters.")]
        public string TeacherFirstName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        [StringLength(20, ErrorMessage = "TeacherLastName cannot exceed 20 characters.")]
        public string TeacherLastName { get; set; }
    }
}
