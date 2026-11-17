using StudentPerformanceAnalysis.DTO.ResultD;

namespace StudentPerformanceAnalysis.Services.Interface
{
    public interface IResultServices
    {
        Task<IEnumerable<ResultDto>> GetAllAsync();

        Task<ResultDto?> GetByIdAsync(int id);
        Task<ResultDto> AddAsync(CreateResultDto model);

        Task<ResultDto?> UpdateAsync(int id, UpdateResultDto model);

        Task DeleteAsync(int id);
    }
}
