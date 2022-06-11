using EmployeesAPI.Data.Models;
using EmployeesAPI.Data.Models.Dto;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetEmployeesByDepartmentName(string name);
        public Task<List<EmployeeDto>> GetEmployeesByCompanyId(int id);
        public Task<int?> AddEmployee(EmployeeDto employee);
        public Task<bool> UpdateEmployee(Employee employee);
        public Task<bool> DeleteEmployee(int id);
    }
}
