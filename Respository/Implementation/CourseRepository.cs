using Microsoft.EntityFrameworkCore;
using StudentPerformanceAnalysis.Context;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;

namespace StudentPerformanceAnalysis.Respository.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Course> AddAsync(Course model)
        {
            var courses = await _db.Courses.AddAsync(model);
            await _db.SaveChangesAsync();
            return courses.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            _db.Courses.Remove(await GetByIdAsync(id));
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _db.Courses
                .AnyAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _db.Courses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _db.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> UpdateAsync(Course model)
        {
            var courses = _db.Courses.Update(model);
            await _db.SaveChangesAsync();
            return courses.Entity;
        }
    }
}
