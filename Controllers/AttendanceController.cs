
using Microsoft.AspNetCore.Mvc;
using StudentPerformanceAnalysis.DTO.AttendanceD;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceServices _serv;

        public AttendanceController(IAttendanceServices serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var attendances = await _serv.GetAllAsync();
            return View(attendances);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateAttendanceDto model)
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
            var attendance = await _serv.GetByIdAsync(id);

            if (attendance == null)
                return NotFound();

            return View(attendance);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attendance = await _serv.GetByIdAsync(id);

            if (attendance == null)
                return NotFound();

            var model = new UpdateAttendanceDto
            {
                StudentId = attendance.StudentId,
                CourseId = attendance.CourseId,
                ClassesAttended = attendance.ClassesAttended,
                TotalClasses = attendance.TotalClasses
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, UpdateAttendanceDto model)
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
            await _serv.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
