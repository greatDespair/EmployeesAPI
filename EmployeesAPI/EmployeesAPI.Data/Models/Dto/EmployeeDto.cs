using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Data.Models.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [RegularExpression(@"^[А-Я][а-яА-Я]*$")]
        public string Surname { get; set; }

        [RegularExpression(@"(8)?[0-9]{10}")]
        public string Phone { get; set; }

        public int CompanyID { get; set; }

        public PassportDto Passport { get; set; }

        public DepartmentDto Department { get; set; }
    }
}
