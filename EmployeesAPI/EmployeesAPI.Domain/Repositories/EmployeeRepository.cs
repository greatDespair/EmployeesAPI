using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesAPI.Data;
using EmployeesAPI.Data.Models;
using EmployeesAPI.Domain.Abstractions;

namespace EmployeesAPI.Domain.Repositories
{
    public class EmployeeRepository : IEmployeeRepository<Employee>
    {

        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }
        public Task<long?> Add(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllByCompanyId(int id)
        {
            var query = "SELECT * FROM Employes"
        }

        public Task<IEnumerable<Employee>> GetAllByDepartment(string depName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
