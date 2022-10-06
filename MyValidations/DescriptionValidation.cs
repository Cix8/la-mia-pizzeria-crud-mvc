using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.MyValidations
{
    public class DescriptionValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string valueToString = (string)value;
            int charCounter = 0;
            if(valueToString != null)
            {
                valueToString = valueToString.Trim();
                for (int i = 0; i < valueToString.Length; i++)
                {
                    if (valueToString[i] == ' ')
                    {
                        charCounter++;
                        int j = i + 1;
                        while (valueToString[j] == ' ')
                        {
                            j++;
                        }
                        i = j - 1;
                    }
                }
            }

            if(charCounter < 4)
            {
                return new ValidationResult("La descrizione deve contenere almeno 5 parole");
            }

            return ValidationResult.Success;
        }
    }
}
