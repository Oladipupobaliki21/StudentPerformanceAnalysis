using Microsoft.AspNetCore.Mvc;
using StudentPerformanceAnalysis.DTO.StudentD;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Services.Implementation
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _repo;
        public StudentServices(IStudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<StudentDto> AddAsync(CreateStudentDto model)
        {

            var exists = await _repo.ExistByMatricNumberAsync(model.MatricNumber.Trim());
            if (exists) 
            {
                throw new InvalidOperationException("A student with this matric number already exist.");
            }

            var departmentExists = await _repo.DepartmentExistsAsync(model.DepartmentId);
            if (!departmentExists)
            {
                throw new ArgumentException("The selected department does not exist");
            }
            var newStudent = new Student
            {
                FullName = model.FullName,
                MatricNumber = model.MatricNumber,
                Gender = model.Gender,
                Level = model.Level,
                DepartmentId = model.DepartmentId
            };

            var created = await _repo.AddAsync(newStudent);
            if (created == null) return null;
            return new StudentDto
            {
                Id = created.Id,
               FullName = created.FullName,
               MatricNumber = created.MatricNumber,
               Gender = created.Gender,
               Level = created.Level,
               DepartmentId = created.DepartmentId
            };
        }

       

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var newStudent = await _repo.GetAllAsync();
            return newStudent.Select(s => new StudentDto
            {
                FullName = s.FullName,
                MatricNumber = s.MatricNumber,
                Gender = s.Gender,
                Level = s.Level,
                DepartmentId = s.DepartmentId
            }
            );
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var created = await _repo.GetByIdAsync(id); 
            if(created == null) return null;
            return new StudentDto
            {
                Id = created.Id,
                FullName = created.FullName,
                MatricNumber = created.MatricNumber,
                Gender = created.Gender,
                Level = created.Level,
                DepartmentId = created.DepartmentId
            };
        }

        public async Task<StudentDto?> UpdateAsync(int id, UpdateStudentDto model)
        {
            var students = await _repo.GetByIdAsync(id);

            //Validate required fields
            if (students == null) return null;
            if (string.IsNullOrWhiteSpace(model.FullName))
            {
                throw new ArgumentException("Student's full name is required.");
            }

            if (string.IsNullOrWhiteSpace(model.MatricNumber))
            {
                throw new ArgumentException("Matriculation number is required.");

            }
            if (string.IsNullOrWhiteSpace(model.Gender))
            {
                throw new ArgumentException("Gender is required.");
            }

            //Validate Level

            if (model.Level < 100 || model.Level > 700)

            {
                throw new ArgumentOutOfRangeException(nameof(model.Level),
                    "Level  must be between 100 and 700");

            }

            //check for duplicate matriculation numbers

            var matricNumber = model.MatricNumber.Trim();
            if(!string.Equals(
                students.MatricNumber,
                matricNumber,
                StringComparison.OrdinalIgnoreCase )&& await _repo.ExistByMatricNumberAsync(matricNumber)
                )
            {
                throw new InvalidOperationException("A student with this matriculation number already existed.");
            }

            //check whether the department exists

            var departmentExists = await _repo.DepartmentExistsAsync(model.DepartmentId);
            if (!departmentExists)
            {
                throw new ArgumentException("The selected department does not exist.");
            }

            //Update the existing student
            students.FullName = model.FullName.Trim();
            students.MatricNumber = matricNumber;
            students.Gender = model.Gender.Trim();
            students.Level = model.Level;
            students.DepartmentId = model.DepartmentId;
            
            await _repo.UpdateAsync(students);
            return new StudentDto
            {
                Id = students.Id,
                FullName = students.FullName,
                MatricNumber = students.MatricNumber,
                Gender = students.Gender,
                Level = students.Level,
                DepartmentId = students.DepartmentId
            };
        }

        Task<bool> IStudentServices.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
