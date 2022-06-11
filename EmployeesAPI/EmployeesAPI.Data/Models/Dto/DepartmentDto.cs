using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Data.Models.Dto
{
    public class DepartmentDto
    {
        public string dName { get; set; }

        [RegularExpression(@"(8)?[0-9]{10}")]
        public string Phone { get; set; }
    }
}
