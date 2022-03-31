using EconomicManagementAPP.Validations;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class AccountTypes
    {//modelo: Se le podrá agregar unas validaciones. Aquí no se pone tamaño de vbles ModelSatate
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]//0 para no específicar el campo requerido. el 0 significa cualquiera, de los campos posteriores
        [FirstCapitalLetter]
        public string Name { get; set; }
        public int UserId { get; set; }
        public int OrderAccount { get; set; }
    }
}
