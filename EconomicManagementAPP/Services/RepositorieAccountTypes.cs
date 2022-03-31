using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace EconomicManagementAPP.Services
{
    public interface IRepositorieAccountTypes //vamos a crear una interface para el servicio de Repositorie
    {
        void Create(AccountTypes accountTypes);
    }
    public class RepositorieAccountTypes : IRepositorieAccountTypes
    {
        private readonly string connectionString;
        public RepositorieAccountTypes(IConfiguration configuration) //Constructor. Vble se inicializa con el link de conexión
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void Create(AccountTypes accountTypes) //Aquí va la query
        {
            using var connection = new SqlConnection(connectionString);  //sugerida dada la importación de SqlClient|| querySingle funciona gracias al Dapper que nos ayuda con la ejecución de las querys
            var id = connection.QuerySingle<int>($@"INSERT INTO AccountTypes 
                                                (Name,UserId, OrderAccount) 
                                                VALUES (@Name, @UserId, @OrderAccount); SELECT SCOPE_IDENTITY();", accountTypes);//name biene del formulario
            accountTypes.Id = id;
        }
}
}
