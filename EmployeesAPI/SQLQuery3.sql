SELECT * FROM Employees
JOIN Department dep ON dep.Id = Employees.DepartmentId
JOIN Passports pass ON pass.Id = Employees.PassportId
WHERE dep.Name = N'Тест';

