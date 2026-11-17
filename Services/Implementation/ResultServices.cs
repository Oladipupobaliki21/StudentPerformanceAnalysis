using StudentPerformanceAnalysis.DTO.ResultD;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Services.Implementation
{
    public class ResultServices : IResultServices
    {
        private readonly IResultRepository _repo;
        public ResultServices(IResultRepository repo)
        {
            _repo = repo;
        }
        public async Task<ResultDto> AddAsync(CreateResultDto model)
        {
            if (model.StudentId <= 0)
            {
                throw new ArgumentException("A valid student is required.");
            }

            if (model.CourseId <= 0)
            {
                throw new ArgumentException("A valid course is required.");
            }

            if (model.Score < 0 || model.Score > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(model.Score),
                    "Score must be between 0 and 100.");
            }

            if (string.IsNullOrWhiteSpace(model.Semester))
            {
                throw new ArgumentException("Semester is required.");
            }

            if (string.IsNullOrWhiteSpace(model.Session))
            {
                throw new ArgumentException("Academic session is required.");
            }

            var studentExists =
                await _repo.StudentExistsAsync(model.StudentId);

            if (!studentExists)
            {
                throw new ArgumentException(
                    "The selected student does not exist.");
            }

            var courseExists =
                await _repo.CourseExistsAsync(model.CourseId);

            if (!courseExists)
            {
                throw new ArgumentException(
                    "The selected course does not exist.");
            }

            var newResult = new Result
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                Score = model.Score,
                Semester = model.Semester.Trim(),
                Session = model.Session.Trim()
            };

            var created = await _repo.AddAsync(newResult);

            return new ResultDto
            {
                Id = created.Id,
                StudentId = created.StudentId,
                CourseId = created.CourseId,
                Score = created.Score,
                Semester = created.Semester,
                Session = created.Session
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResultDto>> GetAllAsync()
        {
            var results = await _repo.GetAllAsync();

            return results.Select(r => new ResultDto
            {
                Id = r.Id,
                StudentId = r.StudentId,
                CourseId = r.CourseId,
                Score = r.Score,
                Semester = r.Semester,
                Session = r.Session
            });
        }

        public async  Task<ResultDto?> GetByIdAsync(int id)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result == null)
            {
                return null;
            }

            return new ResultDto
            {
                Id = result.Id,
                StudentId = result.StudentId,
                CourseId = result.CourseId,
                Score = result.Score,
                Semester = result.Semester,
                Session = result.Session
            };
        }

        public async Task<ResultDto?> UpdateAsync(int id, UpdateResultDto model)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result == null)
            {
                return null;
            }

            if (model.StudentId <= 0)
            {
                throw new ArgumentException("A valid student is required.");
            }

            if (model.CourseId <= 0)
            {
                throw new ArgumentException("A valid course is required.");
            }

            if (model.Score < 0 || model.Score > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(model.Score),
                    "Score must be between 0 and 100.");
            }

            if (string.IsNullOrWhiteSpace(model.Semester))
            {
                throw new ArgumentException("Semester is required.");
            }

            if (string.IsNullOrWhiteSpace(model.Session))
            {
                throw new ArgumentException("Academic session is required.");
            }

            if (!await _repo.StudentExistsAsync(model.StudentId))
            {
                throw new ArgumentException(
                    "The selected student does not exist.");
            }

            if (!await _repo.CourseExistsAsync(model.CourseId))
            {
                throw new ArgumentException(
                    "The selected course does not exist.");
            }

            result.StudentId = model.StudentId;
            result.CourseId = model.CourseId;
            result.Score = model.Score;
            result.Semester = model.Semester.Trim();
            result.Session = model.Session.Trim();

            await _repo.UpdateAsync(result);

            return new ResultDto
            {
                Id = result.Id,
                StudentId = result.StudentId,
                CourseId = result.CourseId,
                Score = result.Score,
                Semester = result.Semester,
                Session = result.Session
            };
        }
    }
}
