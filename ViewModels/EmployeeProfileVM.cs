using Microsoft.AspNetCore.Identity;
using StaffManagement.Models;

namespace StaffManagement.ViewModels
{
    public class EmployeeProfileVM
    {
        public IdentityUser User { get; set; }   
        public List<Employee> Employeees { get; set; }
    }
}
