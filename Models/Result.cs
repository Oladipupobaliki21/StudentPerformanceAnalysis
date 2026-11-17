namespace StudentPerformanceAnalysis.Models
{
    public class Result
    {
        public int Id { get; set; }

        // Foreign Keys
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public double Score { get; set; }

        public string Semester { get; set; } = string.Empty;

        public string Session { get; set; } = string.Empty;

        // Navigation Properties
        public Student Student { get; set; } = null!;

        public Course Course { get; set; } = null!;
    }
}
