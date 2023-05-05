using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PloomesAPI.Infra.Data.Context
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private SqlConnection conn = null;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public IDbConnection GetConnection()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            }

            return conn;
        }
    }
}
