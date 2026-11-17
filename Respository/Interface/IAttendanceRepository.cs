using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.Respository.Interface
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task<Attendance> AddAsync(Attendance model);

        Task<Attendance> UpdateAsync(Attendance model);

        Task DeleteAsync(int id);

        Task<bool> StudentExistsAsync(int studentId);

        Task<bool> CourseExistsAsync(int courseId);
    }
}
