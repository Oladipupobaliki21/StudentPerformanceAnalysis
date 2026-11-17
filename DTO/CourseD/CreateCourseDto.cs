using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.DTO.CourseD
{
    public class CreateCourseDto
    {
        public string Code { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int Unit { get; set; }

        // Navigation Properties
        public List<Result> Results { get; set; } = new();

        public List<Attendance> Attendances { get; set; } = new();
    }
}
