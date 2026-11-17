using StudentPerformanceAnalysis.DTO.DepartmentD;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Services.Implementation
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentServices(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<DepartmentDto> AddAsync(CreateDepartmentDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentException("Department name is required.");
            }

            string departmentName = model.Name.Trim();

            var exist = await _repo.ExistByNameAsync(departmentName);
            if (exist)
            {
                throw new InvalidOperationException("A department with this name already exist.");
            }

            var newDepartment = new Department
            {
                Name = departmentName,
            };

            var created = await _repo.AddAsync(newDepartment);
            if (created == null) return null;
            return new DepartmentDto
            {
                Id = created.Id,
                Name = created.Name,
            };
        }

       

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var newDepartment = await _repo.GetAllAsync();
            return newDepartment.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
            }
            );
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var created = await _repo.GetByIdAsync(id);
            if (created == null) return null;
            return new DepartmentDto
            {
                Id = created.Id,
                Name = created.Name,
            };
        }

        public async Task<DepartmentDto> UpdateAsync(int id, UpdateDepartmentDto model)
        {


            var departments = await _repo.GetByIdAsync(id);
            if (departments == null) return null;

            if(string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentException("Department name is required.");
            }

             string departmentsName = model.Name.Trim();

            bool nameHasChanged = !string.Equals(
                departments.Name,
                departmentsName,

                StringComparison.OrdinalIgnoreCase);

            if(nameHasChanged && await _repo.ExistByNameAsync(departmentsName))
            {
                throw new InvalidOperationException("A department with this name already exists.");
            }

            departments.Name = departmentsName;

            await _repo.UpdateAsync(departments);
            return new DepartmentDto
            {
                Id = departments.Id,
                Name = departments.Name
            };
        }
    }
}
