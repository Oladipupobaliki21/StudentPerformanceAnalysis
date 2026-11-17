
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceAnalysis.Context;
using StudentPerformanceAnalysis.ViewModels;

namespace StudentPerformanceAnalysis.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();

            // Summary Cards
            model.TotalStudents = await _context.Students.CountAsync();
            model.TotalCourses = await _context.Courses.CountAsync();

            // Retrieve all recorded scores
            var scores = await _context.Results
                .Select(r => r.Score)
                .ToListAsync();

            if (scores.Any())
            {
                // Mean / Average
                model.Mean = scores.Average();
                model.AverageScore = model.Mean;

                // Highest Score
                model.HighestScore = scores.Max();

                // Median
                var sortedScores = scores.OrderBy(s => s).ToList();

                int count = sortedScores.Count;

                if (count % 2 == 0)
                {
                    model.Median =
                        (sortedScores[count / 2 - 1] +
                         sortedScores[count / 2]) / 2;
                }
                else
                {
                    model.Median = sortedScores[count / 2];
                }

                // Standard Deviation (Population)
                double variance = scores
                    .Average(s => Math.Pow(s - model.Mean, 2));

                model.StandardDeviation = Math.Sqrt(variance);
            }

            // Performance by Course
            model.CoursePerformances = await _context.Courses
                .Where(c => c.Results.Any())
                .Select(c => new CoursePerformanceViewModel
                {
                    CourseName = c.Title,
                    AverageScore = c.Results.Average(r => r.Score)
                })
                .ToListAsync();

            return View(model);
        }
    }
}
