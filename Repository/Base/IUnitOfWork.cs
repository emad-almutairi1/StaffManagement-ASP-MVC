

using StaffManagement.Models;

namespace StaffManagement.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IEmpRepo Employees { get; }
        IRepository<Project> Projects { get; }
        IRepository<TaskItem> TaskItems { get; }
        IRepository<LeaveRequest> LeaveRequests { get; }
        IRepository<Evaluation> Evaluations { get; }

        int CommitChanges();
    }
}
