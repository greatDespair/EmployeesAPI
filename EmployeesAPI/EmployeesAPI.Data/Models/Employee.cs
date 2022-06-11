using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [RegularExpression(@"^[А-Я][а-яА-Я]*$")]
        public string Name { get; set; }

        [RegularExpression(@"^[А-Я][а-яА-Я]*$")]
        public string Surname { get; set; }

        [RegularExpression(@"(8)?[0-9]{10}")]
        public string Phone { get; set; }

        public int CompanyID { get; set; }

        public int? PassportId { get; set; }

        public int? DepartmentId { get; set; }

        public Passport Passport { get; set; }

        public Department Department { get; set; }
    }
}
