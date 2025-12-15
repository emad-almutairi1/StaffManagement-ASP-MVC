using StaffManagement.Models;

namespace StaffManagement.ViewModels
{
    public class DashboardViewModel
    {
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public int TasksCount { get; set; }
        public int LeaveRequestsCount { get; set; }
        public int EvaluationsCount { get; set; }
        public object Description { get; internal set; }
        public object StartDate { get; internal set; }
        public List<Employee> Employees { get; set; }
    }
}
