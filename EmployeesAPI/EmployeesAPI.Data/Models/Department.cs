using System.ComponentModel.DataAnnotations;


namespace EmployeesAPI.Data.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string dName { get; set; }

        [RegularExpression(@"(8)?[0-9]{10}")]
        public string Phone { get; set; }
    }
}
