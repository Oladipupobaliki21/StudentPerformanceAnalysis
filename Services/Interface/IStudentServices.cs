using StudentPerformanceAnalysis.DTO.StudentD;

namespace StudentPerformanceAnalysis.Services.Interface
{
    public interface IStudentServices
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task<StudentDto> AddAsync(CreateStudentDto model);
        Task<StudentDto?> UpdateAsync(int id, UpdateStudentDto model);
        Task <bool>DeleteAsync(int id);
    }
}
