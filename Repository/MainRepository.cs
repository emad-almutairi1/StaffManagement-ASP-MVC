using StaffManagement.Data;
using StaffManagement.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Models;

namespace StaffManagement.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public MainRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOne(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void DeleteOne(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> FindAll(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll(params string[] agers)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T FindID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public Task<IEnumerable<T>> FindIdAsync(params string[] agers)
        {
            throw new NotImplementedException();
        }

       
        public void UpdateOne(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        object IRepository<T>.FindAll()
        {
            return FindAll();
        }

        public IEnumerable<LeaveRequest> GetLeaves(string userId)
        {
            return _context.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.Employee.UserId == userId)
                .ToList();
        }
    }
}
