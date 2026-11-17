namespace StudentPerformanceAnalysis.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int Unit { get; set; }

        // Navigation Properties
        public List<Result> Results { get; set; } = new();

        public List<Attendance> Attendances { get; set; } = new();

    }
}
