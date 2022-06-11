using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Data.Models.Dto
{
    public class PassportDto
    {
        public string Type { get; set; }

        [RegularExpression(@"^[0-9]*$")]
        public string Number { get; set; }
    }
}
