
namespace EmployeesAPI.Domain.Abstractions
{
    public interface IRepository<T>
    {
        public Task<int?> Add(T item);
        public Task<bool> Update(T item);
        public Task<bool> Delete(int id);
    }
}
