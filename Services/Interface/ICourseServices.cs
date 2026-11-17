using StudentPerformanceAnalysis.DTO.CourseD;

namespace StudentPerformanceAnalysis.Services.Interface
{
    public interface ICourseServices
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(int id);
        Task<CourseDto> AddAsync(CreateCourseDto model);
        Task<CourseDto?> UpdateAsync(int id, UpdateCourseDto model);
        Task DeleteAsync(int id);
    }
}
