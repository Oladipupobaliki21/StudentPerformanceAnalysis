using Microsoft.EntityFrameworkCore;
using StudentPerformanceAnalysis.Context;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;

namespace StudentPerformanceAnalysis.Respository.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Student>AddAsync(Student model)
        {
            var student = await _db.Students.AddAsync(model);
            await _db.SaveChangesAsync();
            return student.Entity;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await GetByIdAsync(id);
            if (student == null)
                return false;

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return true;
        }

       

        public async Task<bool> DepartmentExistsAsync(int deparmentId)
        {
            return await _db.Departments
               .AnyAsync(d => d.Id == deparmentId);
        }

        public async Task<bool> ExistByMatricNumberAsync(string matricNumber)
        {
            return await _db.Students
                .AnyAsync(s => s.MatricNumber == matricNumber);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _db.Students
                .AsNoTracking()
                .Include(s => s.Department)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _db.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student?> UpdateAsync(Student model)
        {
            var student =  _db.Students.Update(model);
            await _db.SaveChangesAsync();
            return student.Entity;
        }
    }
}
