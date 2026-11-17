using Microsoft.EntityFrameworkCore;
using StudentPerformanceAnalysis.Context;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;

namespace StudentPerformanceAnalysis.Respository.Implementation
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _db;
        public ResultRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public  async Task<Result> AddAsync(Result model)
        {
            var results = await _db.Results.AddAsync(model);
            await _db.SaveChangesAsync();
            return results.Entity;
        }

        public async Task<bool> CourseExistsAsync(int courseId)
        {
            return await _db.Courses
                .AnyAsync(c => c.Id == courseId);
        }

        public async Task DeleteAsync(int id)
        {
            _db.Results.Remove(await GetByIdAsync(id));
        }

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            return await _db.Results
                 .AsNoTracking()
                 .Include(r => r.Student)
                 .Include(r => r.Course)
                 .ToListAsync();
        }

        public async Task<Result?> GetByIdAsync(int id)
        {
                        return await _db.Results
                .Include(r => r.Student)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<bool> StudentExistsAsync(int studentId)
        {
            return await _db.Students
                .AnyAsync(s => s.Id == studentId);
        }

        public async  Task<Result> UpdateAsync(Result model)
        {
            var results = _db.Results.Update(model);
            await _db.SaveChangesAsync();
            return results.Entity;
        }
    }
}
