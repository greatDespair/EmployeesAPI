using System.Data;
using Dapper;
using EmployeesAPI.Data;
using EmployeesAPI.Data.Models;
using EmployeesAPI.Domain.Abstractions;

namespace EmployeesAPI.Domain.Repositories
{
    public class EmployeeRepository : IRepository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(Employee item)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var queryPassport = "INSERT INTO Passports (Type, Number) OUTPUT INSERTED.Id VALUES (@Type, @Number);";
                    var queryDepartment = "INSERT INTO Department (dName, Phone) OUTPUT INSERTED.Id VALUES (@dName, @Phone);";
                    var queryEmployee = "INSERT INTO Employees (Name, Surname, Phone, CompanyId, PassportId, DepartmentId) " +
                        "VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportId, @DepartmentId)" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";

                    var checkPassport = "SELECT COUNT(*) FROM Passports WHERE Id = @id;";
                    var checkDepartment = "SELECT COUNT(*) FROM Department WHERE Id = @id;";

                    int passport = item.PassportId ?? 0;
                    int department = item.DepartmentId ?? 0;
                    if ((await connection.QueryFirstAsync<int>(checkPassport, new { id = item.PassportId }) == 0)
                        && (item.Passport != null))
                    {
                        passport = await connection.QueryFirstAsync<int>(queryPassport, new
                        {
                            Type = item.Passport.Type,
                            Number = item.Passport.Number
                        });
                    }

                    if ((await connection.QueryFirstAsync<int>(checkDepartment, new { id = item.DepartmentId }) == 0)
                        && (item.Department != null))
                    {
                        department = await connection.QueryFirstAsync<int>(queryDepartment, new
                        {
                            dName = item.Department.dName,
                            Phone = item.Department.Phone
                        });
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("Name", item.Name, DbType.String);
                    parameters.Add("Surname", item.Surname, DbType.String);
                    parameters.Add("Phone", item.Phone, DbType.String);
                    parameters.Add("CompanyId", item.CompanyID, DbType.Int32);
                    parameters.Add("PassportId", passport, DbType.Int32);
                    parameters.Add("DepartmentId", department, DbType.Int32);

                    var resultId = await connection.QuerySingleAsync<int>(queryEmployee, parameters);

                    return resultId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var checkEmployee = "SELECT COUNT(*) FROM Employees WHERE Id = @id;";
                    if (await connection.QueryFirstAsync<int>(checkEmployee, new { id = id }) == 0)
                    {
                        return false;
                    }

                    var queryDelEmployee = "DELETE FROM Employees WHERE Id = @Id;";
                    var queryGetPassport = "SELECT PassportId FROM Employees WHERE Id = @Id;";

                    var passport = await connection.QueryAsync<int>(queryGetPassport, new { Id = id });
                    var result = await connection.ExecuteAsync(queryDelEmployee, new { Id = id });

                    if (passport != null)
                    {
                        var queryDelPassport = "DELETE FROM Passports WHERE Id = @Id;";
                        await connection.ExecuteAsync(queryDelPassport, new { Id = passport });
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllByCompanyId(int id)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {

                    var queryByNameDep = "SELECT * FROM Employees " +
                        "WHERE CompanyId = @Id;";
                    var queryDep = "SELECT * FROM Department " +
                        "WHERE Id = @DepId";
                    var queryPass = "SELECT * FROM Passports " +
                        "WHERE Id = @PassId";
                    var result = await connection.QueryAsync<Employee>(queryByNameDep, new { Id = id });

                    foreach (Employee employee in result)
                    {
                        var department = await connection.QueryFirstAsync<Department>(queryDep, new { DepId = employee.DepartmentId });
                        var passport = await connection.QueryFirstAsync<Passport>(queryPass, new { PassId = employee.PassportId });
                        employee.Department = department;
                        employee.Passport = passport;
                    }

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllByDepartmentName(string depName)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {

                    var queryByNameDep = "SELECT * FROM Employees " +
                        "JOIN Department requiredDep ON requiredDep.Id = Employees.DepartmentId " +
                        "JOIN Passports requiredPass ON requiredPass.Id = Employees.PassportId " +
                        "WHERE requiredDep.dName = @depname;";
                    var queryDep = "SELECT * FROM Department " +
                        "WHERE Id = @DepId";
                    var queryPass = "SELECT * FROM Passports " +
                        "WHERE Id = @PassId";
                    var result = await connection.QueryAsync<Employee>(queryByNameDep, new { depname = depName });

                    foreach (Employee employee in result)
                    {
                        var department = await connection.QueryFirstAsync<Department>(queryDep, new { DepId = employee.DepartmentId });
                        var passport = await connection.QueryFirstAsync<Passport>(queryPass, new { PassId = employee.PassportId });
                        employee.Department = department;
                        employee.Passport = passport;
                    }

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> Update(Employee item)
        {
            using (var connection = _context.CreateConnection())
            {
                var checkEmployee = "SELECT COUNT(*) FROM Employees WHERE Id = @id;";
                var checkDepartment = "SELECT COUNT(*) FROM Department WHERE Id = @id;";
                var checkPassport = "SELECT COUNT(*) FROM Passports WHERE Id = @id;";

                if (await connection.QueryFirstAsync<int>(checkEmployee, new { id = item.Id }) == 0 ||
                        await connection.QueryFirstAsync<int>(checkDepartment, new { id = item.DepartmentId }) == 0 ||
                        await connection.QueryFirstAsync<int>(checkPassport, new { id = item.PassportId }) == 0)
                {
                    return false;
                }

                var queryUpdEmployee = "UPDATE Employees SET " +
                    "Name = COALESCE(@Name,Name), " +
                    "Surname = COALESCE(@Surname, Surname), " +
                    "Phone = COALESCE(@Phone, Phone), " +
                    "CompanyId = COALESCE(@CompanyId, CompanyId), " +
                    "DepartmentId = COALESCE(@DepartmentId, DepartmentId), " +
                    "PassportId = COALESCE(@PassportId, PassportId) " +
                    "WHERE Id = @Id;";

                var queryUpdPassport = "UPDATE Passports SET " +
                    "Number = COALESCE(@Number, Number), " +
                    "Type = COALESCE(@Type, Type) " +
                    "WHERE Id = @PassportId;";

                var queryUpdDepartment = "UPDATE Department SET " +
                    "dName = COALESCE(@Name, dName), " +
                    "Phone = COALESCE(@Phone, Phone) " +
                    "WHERE Id = @DepartmentId;";

                if (item.Department != null)
                {
                    await connection.ExecuteAsync(queryUpdDepartment, new
                    {
                        DepartmentId = item.Department.Id,
                        Name = item.Department.dName,
                        Phone = item.Department.Phone
                    });
                }

                if (item.Passport != null)
                {
                    await connection.ExecuteAsync(queryUpdPassport, new
                    {
                        PassportId = item.Passport.Id,
                        Type = item.Passport.Type,
                        Number = item.Passport.Number
                    });
                }

                await connection.ExecuteAsync(queryUpdEmployee, new
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Phone = item.Phone,
                    DepartmentId = item.DepartmentId,
                    PassportId = item.PassportId,
                    CompanyId = item.CompanyID
                });

                return true;
            }
        }
    }
}
