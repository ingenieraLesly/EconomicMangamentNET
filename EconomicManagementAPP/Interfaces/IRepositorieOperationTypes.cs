using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interfaces
{
    public interface IRepositorieOperationTypes
    {
        Task Create(OperationTypes operationType);
        Task<bool> Exist(string description);
        Task<OperationTypes> GetOperationTypeById(int id);
        Task<IEnumerable<OperationTypes>> GetOperationTypes();
        Task Modify(OperationTypes operationType);
        Task Delete(int id);
    }
}
