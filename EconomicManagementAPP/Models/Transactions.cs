using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Transaction Date")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));
        public decimal Total { get; set; }
        public int OperationTypeId { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
