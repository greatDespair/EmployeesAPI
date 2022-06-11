using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Data.Models.Dto
{
    public class PassportDto
    {
        public string Type { get; set; }

        [RegularExpression(@"^[0-9]*$")]
        public string Number { get; set; }
    }
}
