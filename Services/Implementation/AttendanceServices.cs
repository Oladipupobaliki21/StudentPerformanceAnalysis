using StudentPerformanceAnalysis.DTO.AttendanceD;
using StudentPerformanceAnalysis.Models;
using StudentPerformanceAnalysis.Respository.Interface;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Services.Implementation
{
    public class AttendanceServices : IAttendanceServices
    {
        private readonly IAttendanceRepository _repo;

        public AttendanceServices(IAttendanceRepository repo)
        {
            _repo = repo;
        }
        public async Task<AttendanceDto> AddAsync(CreateAttendanceDto model)
        {
            if (model.ClassesAttended < 0)
                throw new ArgumentException(
                    "Classes attended cannot be negative.");

            if (model.TotalClasses <= 0)
                throw new ArgumentException(
                    "Total classes must be greater than zero.");

            if (model.ClassesAttended > model.TotalClasses)
                throw new ArgumentException(
                    "Classes attended cannot exceed total classes.");

            var studentExists =
                await _repo.StudentExistsAsync(model.StudentId);

            if (!studentExists)
                throw new ArgumentException(
                    "The selected student does not exist.");

            var courseExists =
                await _repo.CourseExistsAsync(model.CourseId);

            if (!courseExists)
                throw new ArgumentException(
                    "The selected course does not exist.");

            var attendance = new Attendance
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                ClassesAttended = model.ClassesAttended,
                TotalClasses = model.TotalClasses
            };

            var created = await _repo.AddAsync(attendance);

            return new AttendanceDto
            {
                Id = created.Id,
                StudentId = created.StudentId,
                CourseId = created.CourseId,
                ClassesAttended = created.ClassesAttended,
                TotalClasses = created.TotalClasses
            };
        }

        public async  Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            var attendances = await _repo.GetAllAsync();

            return attendances.Select(a => new AttendanceDto
            {
                Id = a.Id,
                StudentId = a.StudentId,
                CourseId = a.CourseId,
                ClassesAttended = a.ClassesAttended,
                TotalClasses = a.TotalClasses
            });
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _repo.GetByIdAsync(id);

            if (attendance == null)
                return null;

            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                CourseId = attendance.CourseId,
                ClassesAttended = attendance.ClassesAttended,
                TotalClasses = attendance.TotalClasses
            };
        }

        public async Task<AttendanceDto?> UpdateAsync(int id, UpdateAttendanceDto model)
        {
            var attendance = await _repo.GetByIdAsync(id);

            if (attendance == null)
                return null;

            if (model.ClassesAttended < 0)
                throw new ArgumentException(
                    "Classes attended cannot be negative.");

            if (model.TotalClasses <= 0)
                throw new ArgumentException(
                    "Total classes must be greater than zero.");

            if (model.ClassesAttended > model.TotalClasses)
                throw new ArgumentException(
                    "Classes attended cannot exceed total classes.");

            var studentExists =
                await _repo.StudentExistsAsync(model.StudentId);

            if (!studentExists)
                throw new ArgumentException(
                    "The selected student does not exist.");

            var courseExists =
                await _repo.CourseExistsAsync(model.CourseId);

            if (!courseExists)
                throw new ArgumentException(
                    "The selected course does not exist.");

            attendance.StudentId = model.StudentId;
            attendance.CourseId = model.CourseId;
            attendance.ClassesAttended = model.ClassesAttended;
            attendance.TotalClasses = model.TotalClasses;

            await _repo.UpdateAsync(attendance);

            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                CourseId = attendance.CourseId,
                ClassesAttended = attendance.ClassesAttended,
                TotalClasses = attendance.TotalClasses
            };
        }
    }
}
