
using Microsoft.AspNetCore.Mvc;
using StudentPerformanceAnalysis.DTO.StudentD;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentServices _serv;

        public StudentController(IStudentServices serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _serv.GetAllAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _serv.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _serv.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _serv.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            var model = new UpdateStudentDto
            {
                FullName = student.FullName,
                MatricNumber = student.MatricNumber,
                Gender = student.Gender,
                Level = student.Level,
                DepartmentId = student.DepartmentId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, UpdateStudentDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var updated = await _serv.UpdateAsync(id, model);

                if (updated == null)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _serv.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
