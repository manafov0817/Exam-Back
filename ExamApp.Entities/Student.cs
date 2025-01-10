using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Entities
{
    public class Student : BaseEntity
    {
        [Column(TypeName = "int")]
        [Range(1, 99999, ErrorMessage = "Number must be between 1 and 99999.")]
        public int Number { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        [StringLength(30, ErrorMessage = "FirstName cannot exceed 30 characters.")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [Required]
        [StringLength(30, ErrorMessage = "LastName cannot exceed 30 characters.")]
        public string LastName { get; set; }

        // Class is now an int with a range validation of 1 to 99
        [Column(TypeName = "int")]
        [Range(1, 99, ErrorMessage = "Class must be between 1 and 99.")]
        public int Class { get; set; }
    }
}
