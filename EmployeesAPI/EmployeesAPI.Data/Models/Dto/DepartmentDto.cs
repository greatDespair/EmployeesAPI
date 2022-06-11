using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Data.Models.Dto
{
    public class DepartmentDto
    {
        public string dName { get; set; }

        [RegularExpression(@"(8)?[0-9]{10}")]
        public string Phone { get; set; }
    }
}
