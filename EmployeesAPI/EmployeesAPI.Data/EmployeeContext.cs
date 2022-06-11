using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EmployeesAPI.Data
{
    public class EmployeeContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
