using StaffManagement.Data;
using StaffManagement.Models;
using StaffManagement.Repository.Base;

namespace StaffManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmpRepo(_context);
            Projects = new MainRepository<Project>(_context);
            TaskItems = new MainRepository<TaskItem>(_context);
            LeaveRequests = new MainRepository<LeaveRequest>(_context);
            Evaluations = new MainRepository<Evaluation>(_context);
        }

        public IEmpRepo Employees { get; private set; }
        public IRepository<TaskItem> TaskItems { get; private set; }
        public IRepository<Project> Projects { get; private set; }
        public IRepository<Evaluation> Evaluations { get; private set; }
        public IRepository<LeaveRequest> LeaveRequests { get; private set; }


        int IUnitOfWork.CommitChanges()
        {
            return _context.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}
