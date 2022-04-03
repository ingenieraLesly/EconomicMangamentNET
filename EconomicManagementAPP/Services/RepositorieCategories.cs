using Dapper;
using EconomicManagementAPP.Interfaces;
using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public class RepositorieCategories : IRepositorieCategories
    {
        private readonly string connectionString;
        public RepositorieCategories(IConfiguration configuration) 
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(Categories categorie) 
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Categories 
                                                              (Name, OperationTypeId, UserId) VALUES
                                                              (@Name, @OperationTypeId, @UserId); 
                                                              SELECT SCOPE_IDENTITY();", categorie);
            categorie.Id = id;
        }
        public async Task<bool> Exist(string name, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM Categories
                                                                         WHERE Name = @name AND UserId = @userId",
                                                                         new { name, userId });
            return exist == 1;
        }
        public async Task<Categories> GetCategorieById(int id, int userId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Categories>(@"SELECT Id, Name, OperationTypeId, UserId
                                                                           FROM Categories
                                                                           WHERE Id = @id AND UserId = @userId",
                                                                           new { id, userId });
        }

        public async Task<bool> CategorieIsUsed(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var used = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM Categories c
                                                                JOIN Transactions t ON t.CategoryId = c.Id
                                                                WHERE c.Id = @id", new { id });
            return used == 1;
        }

        public async Task<IEnumerable<Categories>> GetCategories(int userId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Categories>(@"SELECT c.Id, c.Name, ot.Description AS OperationType, c.UserId
                                                             FROM Categories c
                                                             JOIN OperationTypes ot ON ot.Id = c.OperationTypeId
                                                             WHERE c.UserId = @userId OR c.UserId = 0", new { userId });
        }

        public async Task<IEnumerable<Categories>> GetCategories(int userId, int operationTypeId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Categories>(@"SELECT c.Id, c.Name, ot.Description AS OperationType, c.UserId
                                                             FROM Categories c
                                                             JOIN OperationTypes ot ON ot.Id = c.OperationTypeId
                                                             WHERE (c.UserId = @userId OR c.UserId = 0) AND c.OperationTypeId = @operationTypeId", new { userId, operationTypeId });
        }

        public async Task Modify(Categories categorie)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Categories SET 
                                            Name = @Name,
                                            OperationTypeId = @OperationTypeID
                                            WHERE Id = @Id", categorie);
        }
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Categories WHERE Id = @Id", new { id });
        }
    }
}
