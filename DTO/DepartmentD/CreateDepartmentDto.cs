using StudentPerformanceAnalysis.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentPerformanceAnalysis.DTO.DepartmentD
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        //Navigation Property  - One Department has many students
        public List<Student> Students { get; set; } = new();
    }
}
