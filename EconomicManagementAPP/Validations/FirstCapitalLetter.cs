using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Validations
{
    public class FirstCapitalLetter: ValidationAttribute //Validaciones para atributos para campos de formularios
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext) 
        { 
            //eliminar al optimizar para hacer esto desde el front y sacar de aquí
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter =  value.ToString()[0].ToString(); //le pide al usuario que ingrese la letra inicial sea mayúscula, en la vble queda la primera letra
            //ahora vamos a validar a esa primera letra contenida en la vble firstLetter
            if (firstLetter != firstLetter.ToUpper())// si la vble es diferente a la vble en mayúscula entonces
            {
                return new ValidationResult("The first letter must be in uppercase");
            }
            return ValidationResult.Success; //Sino, continúe porque lo anterior es false y no entra allá
        }
    }
}
