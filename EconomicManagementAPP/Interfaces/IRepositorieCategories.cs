using EconomicManagementAPP.Models;

namespace EconomicManagementAPP.Interfaces
{
    public interface IRepositorieCategories 
    {
        Task Create(Categories categorie);
        Task<bool> Exist(string name, int userId);
        Task<Categories> GetCategorieById(int id, int userId);
        Task<IEnumerable<Categories>> GetCategories(int userId);
        Task Modify(Categories categorie);
        Task Delete(int id);
        Task<IEnumerable<Categories>> GetCategories(int userId, int operationTypeId);
        Task<bool> CategorieIsUsed(int id);
    }
}
