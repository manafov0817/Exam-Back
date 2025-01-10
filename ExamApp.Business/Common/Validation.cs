using System.ComponentModel.DataAnnotations;

namespace ExamApp.Business.Common
{
    public static class Validation
    {
        public static void Validate(object entity)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(entity);

            if (!Validator.TryValidateObject(entity, validationContext, validationResults, true))
                // Collect validation errors and throw an exception
                throw new ValidationException($"Validation failed: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
        }
    }
}
