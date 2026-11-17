using Microsoft.VisualBasic;
using StudentPerformanceAnalysis.DTO.CourseD;
using StudentPerformanceAnalysis.DTO.StudentD;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Services.Implementation
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _repo;
        public CourseServices(ICourseRepository repo)
        {
            _repo = repo;
        }
        public async Task<CourseDto> AddAsync(CreateCourseDto model)
        {

            if(string.IsNullOrWhiteSpace(model.Code))
            {
                throw new ArgumentException("Course code is required.");
            }

            if (string.IsNullOrWhiteSpace(model.Title))
            {
                throw new ArgumentException("Course title is required.");
            }

            if (model.Unit <= 0)
            {
                throw new ArgumentException("Course unit must be greater than zero.");
            }

             string courseCode = model.Code.Trim();

            var exists = await _repo.ExistsByCodeAsync(courseCode);
            if (exists) 
            {
                throw new InvalidOperationException("A course wit this code already exists.");
            }

            var newCourse = new Course
            {
                Code = model.Code,
                Title = model.Title,
                Unit = model.Unit,
                
            };

            var created = await _repo.AddAsync(newCourse);
            if (created == null) return null;
            return new CourseDto
            {
                Id = created.Id,
                Code = created.Code,
                Title = created.Title,
                Unit = created.Unit,
                
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var newCourse = await _repo.GetAllAsync();
            return newCourse.Select(c => new CourseDto
            {
                Code = c.Code,
                Title = c.Title,
                Unit = c.Unit
            }
            );
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var created = await _repo.GetByIdAsync(id);
            if (created == null) return null;
            return new CourseDto
            {
                Id = created.Id,
                Code = created.Code,
                Title  = created.Title,
                Unit = created.Unit
            };
        }

        public async Task<CourseDto?> UpdateAsync(int id, UpdateCourseDto model)
        {
            var course = await _repo.GetByIdAsync(id);

            if (course == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(model.Code))
            {
                throw new ArgumentException("Course code is required.");
            }

            if (string.IsNullOrWhiteSpace(model.Title))
            {
                throw new ArgumentException("Course title is required.");
            }

            if (model.Unit <= 0)
            {
                throw new ArgumentException(
                    "Course unit must be greater than zero.");
            }

            string courseCode = model.Code.Trim();

            bool codeHasChanged = !string.Equals(
                course.Code,
                courseCode,
                StringComparison.OrdinalIgnoreCase);

            if (codeHasChanged &&
                await _repo.ExistsByCodeAsync(courseCode))
            {
                throw new InvalidOperationException(
                    "A course with this code already exists.");
            }

            course.Code = courseCode;
            course.Title = model.Title.Trim();
            course.Unit = model.Unit;

            await _repo.UpdateAsync(course);

            return new CourseDto
            {
                Id = course.Id,
                Code = course.Code,
                Title = course.Title,
                Unit = course.Unit
            };

        }
    }
}
