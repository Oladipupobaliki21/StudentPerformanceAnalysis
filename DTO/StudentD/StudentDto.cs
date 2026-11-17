using StudentPerformanceAnalysis.Models;

namespace StudentPerformanceAnalysis.DTO.StudentD
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string MatricNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Level { get; set; }

        //ForeignKey 
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        //Navigation Properties
        public List<Result> Results { get; set; } = new();
        public List<Attendance> Attendances { get; set; } = new();
    }
}
