
using Microsoft.AspNetCore.Mvc;
using StudentPerformanceAnalysis.DTO.ResultD;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultServices _serv;

        public ResultController(IResultServices serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var results = await _serv.GetAllAsync();
            return View(results);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateResultDto model)
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
            var result = await _serv.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _serv.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            var model = new UpdateResultDto
            {
                StudentId = result.StudentId,
                CourseId = result.CourseId,
                Score = result.Score,
                Semester = result.Semester,
                Session = result.Session
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, UpdateResultDto model)
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
