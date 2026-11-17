using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.Respository.Interface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> AddAsync(Course model);
        Task<Course> UpdateAsync(Course model);
        Task DeleteAsync(int id);
        Task<bool> ExistsByCodeAsync(string code);
    }
}
