using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StudentPerformanceAnalysis.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Navigation Property  - One Department has many students
        public List<Student> Students { get; set; } = new();

    }
}
