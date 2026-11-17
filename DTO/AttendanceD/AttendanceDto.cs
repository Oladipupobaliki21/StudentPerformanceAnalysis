using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.DTO.AttendanceD
{
    public class AttendanceDto
    {
        public int Id { get; set; }

        // Foreign Keys
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public int ClassesAttended { get; set; }

        public int TotalClasses { get; set; }

        // Navigation Properties
        public Student Student { get; set; } = null!;

        public Course Course { get; set; } = null!;
    }
}
