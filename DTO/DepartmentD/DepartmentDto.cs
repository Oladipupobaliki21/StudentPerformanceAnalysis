using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.DTO.DepartmentD
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Navigation Property  - One Department has many students
        public List<Student> Students { get; set; } = new();
    }
}
