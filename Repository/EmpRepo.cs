using Microsoft.EntityFrameworkCore;
using StaffManagement.Data;
using StaffManagement.Models;
using StaffManagement.Repository.Base;
using StaffManagement.ViewModels;
using System;

namespace StaffManagement.Repository
{
    public class EmpRepo : MainRepository<Employee>, IEmpRepo
    {
        public EmpRepo(ApplicationDbContext context) : base(context)
        {
        }

        public void AddOne(EmployeeCreateVM employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetByUserId(string userId)
        {
            return _context.Employees.FirstOrDefault(e => e.UserId == userId);
        }

        public async Task<Employee> GetByUserIdAsync(string userId)
        {
            return await _context.Employees
                           .FirstOrDefaultAsync(e => e.UserId == userId); 
        }

        public Employee GetEmployeeWithDetails(string userId)
        {
            return _context.Employees
        .Include(e => e.Projects)
        .Include(e => e.LeaveRequests)
        .Include(e => e.Evaluations)
        .Include(e => e.TaskItems)
         .ThenInclude(t => t.Project) 
        .FirstOrDefault(e => e.UserId == userId); 
        }

      

        public decimal GetSalary(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void SetPayroll(Employee employee)
        {
            throw new NotImplementedException();
        }

        object IEmpRepo.FindAll()
        {
            return FindAll();
        }
    }
}
