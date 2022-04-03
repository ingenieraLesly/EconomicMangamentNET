using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class Accounts
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public Decimal Balance { get; set; }
        public string Description { get; set; }
    }
}
