using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.Respository.Interface
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> AddAsync(Department model);
        Task<Department> UpdateAsync(Department model);
        Task DeleteAsync(int id);
        Task<bool> ExistByNameAsync(string name);
       
    }
}
