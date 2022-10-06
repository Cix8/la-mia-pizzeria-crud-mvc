using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.MyValidations
{
    public class PriceValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            float fieldValue = (float)value;

            if(fieldValue <= 0)
            {
                return new ValidationResult("Il prezzo non può essere negativo o uguale a zero");
            }

            return ValidationResult.Success;
        }
    }
}
