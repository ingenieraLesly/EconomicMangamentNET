using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Models
{
    public class CategoriesViewModel : Categories
    {
        public IEnumerable<SelectListItem> OperationTypes { get; set; }
    }
}
