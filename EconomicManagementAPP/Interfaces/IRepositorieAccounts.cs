using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interfaces
{
    public interface IRepositorieAccounts
    {
        Task Create(Accounts account);
        Task<bool> Exist(string name, int userId);
        Task<IEnumerable<Accounts>> GetAccounts(int userId);
        Task<Accounts> GetFirstAccount(int userId);
        Task Modify(Accounts account);
        Task<Accounts> GetAccountById(int id, int userId);
        Task Delete(int id);
    }
}
