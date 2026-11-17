
using Microsoft.AspNetCore.Mvc;
using StudentPerformanceAnalysis.DTO.DepartmentD;
using StudentPerformanceAnalysis.Services.Interface;

namespace StudentPerformanceAnalysis.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _serv;

        public DepartmentController(IDepartmentServices serv)
        {
            _serv = serv;
        }

        // Display all departments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _serv.GetAllAsync();

            return View(departments);
        }

        // Display the create form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Process the create form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateDepartmentDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _serv.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        // Display one department
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var department = await _serv.GetByIdAsync(id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        // Display the edit form
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _serv.GetByIdAsync(id);

            if (department == null)
                return NotFound();

            var model = new UpdateDepartmentDto
            {
                Name = department.Name
            };

            return View(model);
        }

        // Process the edit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            UpdateDepartmentDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var department = await _serv.UpdateAsync(id, model);

            if (department == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // Delete a department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _serv.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
