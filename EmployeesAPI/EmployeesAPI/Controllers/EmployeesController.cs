using EmployeesAPI.Data.Models;
using EmployeesAPI.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository<Employee> _employeeRepo;
        public EmployeesController(IEmployeeRepository<Employee> companyRepo)
        {
            _employeeRepo = companyRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var companies = await _employeeRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
