using Microsoft.EntityFrameworkCore;
using StudentPerformanceAnalysis.Context;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;

namespace StudentPerformanceAnalysis.Respository.Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Department> AddAsync(Department model)
        {
            var departments = await _db.Departments.AddAsync(model);
            await _db.SaveChangesAsync();
            return departments.Entity;
        }

        public  async Task DeleteAsync(int id)
        {
            _db.Departments.Remove(await GetByIdAsync(id));
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _db.Departments
                //.Include(d => d.Students)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _db.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public  async Task<Department> UpdateAsync(Department model)
        {
            var departments =  _db.Departments.Update(model);
            await _db.SaveChangesAsync();
            return departments.Entity;
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _db.Departments
                .AnyAsync(d => d.Name == name);
        }
    }
}
