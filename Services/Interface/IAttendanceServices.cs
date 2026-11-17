using StudentPerformanceAnalysis.DTO.AttendanceD;

namespace StudentPerformanceAnalysis.Services.Interface
{
    public interface IAttendanceServices
    {
        Task<AttendanceDto> AddAsync(CreateAttendanceDto model);

        Task<IEnumerable<AttendanceDto>> GetAllAsync();

        Task<AttendanceDto?> GetByIdAsync(int id);

        Task<AttendanceDto?> UpdateAsync(int id,UpdateAttendanceDto model);

        Task DeleteAsync(int id);
    }
}
