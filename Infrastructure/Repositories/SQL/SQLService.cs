using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.SQL
{
    public class SQLService
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public SQLService(IConfiguration config)
        {
            _config = config;
            _connectionString = _config["ConnectionStrings:Master"];
        }

        public async Task SaveDataAsync<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteDataAsync<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}