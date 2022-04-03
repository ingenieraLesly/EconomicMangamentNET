using Dapper;
using EconomicManagementAPP.Interfaces;
using EconomicManagementAPP.Models;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public class RepositorieOperationTypes : IRepositorieOperationTypes
    {
        private readonly string connectionString;
        public RepositorieOperationTypes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(OperationTypes operationType)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO OperationTypes 
                                                            (Description) VALUES
                                                            (@Description); 
                                                            SELECT SCOPE_IDENTITY();", operationType);
            operationType.Id = id;
        }
        public async Task<bool> Exist(string description)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM OperationTypes
                                                                        WHERE Description = @description",
                                                                        new { description });
            return exist == 1;
        }
        public async Task<OperationTypes> GetOperationTypeById(int id)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<OperationTypes>(@"SELECT Id, Description
                                                                               FROM OperationTypes
                                                                               WHERE Id = @id",
                                                                               new { id });
        }
        public async Task<IEnumerable<OperationTypes>> GetOperationTypes()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<OperationTypes>("SELECT Id, Description FROM OperationTypes");
        }
        public async Task Modify(OperationTypes operationType)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE OperationTypes SET 
                                            Description = @Description
                                            WHERE Id = @Id", operationType);
        }
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE OperationTypes WHERE Id = @Id", new { id });
        }
    }
}
