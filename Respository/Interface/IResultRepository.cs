using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.Respository.Interface
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetAllAsync();

        Task<Result?> GetByIdAsync(int id);

        Task<Result> AddAsync(Result model);

        Task<Result>UpdateAsync(Result model);

        Task DeleteAsync(int id);

        Task<bool> StudentExistsAsync(int studentId);

        Task<bool> CourseExistsAsync(int courseId);

    }
}
