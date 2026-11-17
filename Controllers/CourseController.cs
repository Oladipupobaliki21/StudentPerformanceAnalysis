
using Microsoft.AspNetCore.Mvc;
using StudentPerformanceAnalysis.DTO.CourseD;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseServices _serv;

        public CourseController(ICourseServices serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _serv.GetAllAsync();
            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseDto model)
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
            var course = await _serv.GetByIdAsync(id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _serv.GetByIdAsync(id);

            if (course == null)
                return NotFound();

            var model = new UpdateCourseDto
            {
                Code = course.Code,
                Title = course.Title,
                Unit = course.Unit
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, UpdateCourseDto model)
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
