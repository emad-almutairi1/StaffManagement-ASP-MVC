
using StaffManagement.Models;

namespace StaffManagement.ViewModels
{
    public class EmployeeDashboardVM
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public int TasksCount { get; set; }
        public int NotificationsCount { get; set; }
        public string Email { get; set; }
        public DateTime? HireDate { get; set; }

        public int TaskCount { get; set; }
        public int ProjectsCount { get; set; }
        public int EvaluationsCount { get; set; }
        public int LeaveRequestsCount { get; set; }

        public IEnumerable<TaskItem>? LastTasks { get; set; }
    }
}
