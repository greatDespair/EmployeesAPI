using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeRepository<T>
    {
        public Task<IEnumerable<T>> GetAllByDepartment(string depName);
        public Task<IEnumerable<T>> GetAllByCompanyId(int id);
        public Task<long?> Add(T item);
        public Task<bool> Update(T item);
        public Task<bool> Delete(int id);
    }
}
