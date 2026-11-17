using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.Respository.Interface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> AddAsync(Student model);
        Task<Student?> UpdateAsync(Student model);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistByMatricNumberAsync(string matricNumber);
        Task<bool> DepartmentExistsAsync(int deparmentId);
    }
}
