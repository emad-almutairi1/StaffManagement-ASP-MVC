using StaffManagement.Areas.Admin.Controllers;
using StaffManagement.Models;
using StaffManagement.ViewModels;

namespace StaffManagement.Repository.Base
{
    public interface IEmpRepo : IRepository<Employee>
    {
        void SetPayroll(Employee employee);
        decimal GetSalary(Employee employee);
        object FindAll();
        Employee GetByUserId(string userId);
        Task<Employee> GetByUserIdAsync(string userId);
        Employee GetEmployeeWithDetails(string userId);
        void AddOne(EmployeeCreateVM employee);
      
    }
}
