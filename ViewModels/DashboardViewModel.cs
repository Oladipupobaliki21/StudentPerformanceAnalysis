
namespace StudentPerformanceAnalysis.ViewModels
{
    public class DashboardViewModel
    {
        // Summary Cards
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }

        public double AverageScore { get; set; }
        public double PassRate { get; set; }

        // Key Statistics
        public double Mean { get; set; }
        public double Median { get; set; }
        public double StandardDeviation { get; set; }
        public double HighestScore { get; set; }

        // Grade Distribution
        public int GradeA { get; set; }
        public int GradeB { get; set; }
        public int GradeC { get; set; }
        public int GradeD { get; set; }
        public int GradeF { get; set; }

        // Pass / Fail
        public int TotalPass { get; set; }
        public int TotalFail { get; set; }

        // Score Distribution
        public int ScoreBelow50 { get; set; }
        public int Score50To59 { get; set; }
        public int Score60To69 { get; set; }
        public int Score70To79 { get; set; }
        public int Score80To100 { get; set; }

        // Course Performance
        public List<CoursePerformanceViewModel> CoursePerformances { get; set; }
            = new();
    }

    public class CoursePerformanceViewModel
    {
        public string CourseName { get; set; } = string.Empty;
        public double AverageScore { get; set; }
    }
}
