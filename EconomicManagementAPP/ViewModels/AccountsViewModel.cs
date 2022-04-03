using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Models
{
    public class AccountsViewModel : Accounts
    {
        public IEnumerable<SelectListItem> AccountTypes { get; set; }
    }
}
