using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Models
{
    public class TransactionsViewModel : Transactions
    {
        public IEnumerable<SelectListItem> OperationTypes { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
