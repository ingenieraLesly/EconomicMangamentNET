using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interfaces
{
    public interface IRepositorieTransactions
    {
        Task Create(Transactions transaction);
        //Task<bool> Exist(string Name, int UserId);
        Task<Transactions> GetTransactionById(int id, int userId);
        Task<IEnumerable<Transactions>> GetTransactions(int userId);
        Task<IEnumerable<Transactions>> GetTransactions(int accountId, int userId);
        Task Modify(Transactions transaction);
        Task Delete(int id);
    }
}
