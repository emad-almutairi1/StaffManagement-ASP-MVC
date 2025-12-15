using StaffManagement.Models;
using System.Linq.Expressions;

namespace StaffManagement.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        T FindID(int id);
        IEnumerable<T> FindAll(int id);
        void AddOne(T entity);
        void UpdateOne(T entity);
        void DeleteOne(T entity);
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> FindAsync();
        object FindAll();
        IEnumerable<T> FindAll(params string[] agers);
        Task<IEnumerable<T>> FindIdAsync(params string[] agers);
        Task<IEnumerable<T>> FindAllAsync();
        IEnumerable<LeaveRequest> GetLeaves(string userId);
    }
}
