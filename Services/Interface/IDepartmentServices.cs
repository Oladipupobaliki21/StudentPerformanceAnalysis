using StudentPerformanceAnalysis.DTO.DepartmentD;

namespace StudentPerformanceAnalysis.Services.Interface
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<DepartmentDto> AddAsync(CreateDepartmentDto model);
        Task<DepartmentDto> UpdateAsync(int id, UpdateDepartmentDto model);
        Task DeleteAsync(int id);
    }
}
