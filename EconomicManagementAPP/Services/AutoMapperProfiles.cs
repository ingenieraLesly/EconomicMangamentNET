using AutoMapper;
using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Accounts, AccountsIndexViewModel>();
            CreateMap<Accounts, AccountsViewModel>();
            CreateMap<Categories, CategoriesViewModel>();
            CreateMap<Transactions, TransactionsViewModel>();
        }
    }
}
