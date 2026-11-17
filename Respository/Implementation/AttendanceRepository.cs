using Microsoft.EntityFrameworkCore;
using StudentPerformanceAnalysis.Context;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;

namespace StudentPerformanceAnalysis.Respository.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _db;
        public AttendanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Attendance> AddAsync(Attendance model)
        {
            var attendance = await _db.Attendances.AddAsync(model);
            await _db.SaveChangesAsync();
            return attendance.Entity;
        }

        public async Task<bool> CourseExistsAsync(int courseId)
        {
            return await _db.Courses
               .AnyAsync(c => c.Id == courseId);
        }

        public async Task DeleteAsync(int id)
        {
            _db.Attendances.Remove(await GetByIdAsync(id));
        }

        public async  Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _db.Attendances
                .AsNoTracking()
                .Include(a => a.Student)
                .Include(a => a.Course)
                .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _db.Attendances
                .Include(a => a.Student)
                .Include(a => a.Course)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> StudentExistsAsync(int studentId)
        {
            return await _db.Students
                .AnyAsync(s => s.Id == studentId);
        }

        public async Task<Attendance> UpdateAsync(Attendance model)
        {
             var attendance = _db.Attendances.Update(model);
            await _db.SaveChangesAsync();
            return attendance.Entity;
        }
    }
}
