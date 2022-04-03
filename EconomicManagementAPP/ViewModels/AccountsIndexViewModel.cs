using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Models
{
    public class AccountsIndexViewModel
    {
        public IEnumerable<SelectListItem> Accounts { get; set; }
    }
}
