using EmployeesAPI.Data.Models;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllByDepartmentName(string depName);

        public Task<IEnumerable<Employee>> GetAllByCompanyId(int compId);
    }
}
