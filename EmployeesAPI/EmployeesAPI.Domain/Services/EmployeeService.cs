using EmployeesAPI.Data.Models;
using EmployeesAPI.Domain.Abstractions;
using EmployeesAPI.Domain.Repositories;
using AutoMapper;
using EmployeesAPI.Data.Models.Dto;

namespace EmployeesAPI.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IMapper _mapper;
        public EmployeeService(EmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int?> AddEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var result = await _employeeRepository.Add(employee);
            return result;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.Delete(id);
            return result;
        }

        public async Task<List<EmployeeDto>> GetEmployeesByCompanyId(int id)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var result = await _employeeRepository.GetAllByCompanyId(id);
            foreach (var employee in result)
            {
                employees.Add(_mapper.Map<EmployeeDto>(employee));
            }
            return employees;
        }

        public async Task<List<EmployeeDto>> GetEmployeesByDepartmentName(string name)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var result = await _employeeRepository.GetAllByDepartmentName(name);
            foreach(var employee in result)
            {
                employees.Add(_mapper.Map<EmployeeDto>(employee));
            }
            return employees;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var result = await _employeeRepository.Update(employee);
            return result;
        }
    }
}
