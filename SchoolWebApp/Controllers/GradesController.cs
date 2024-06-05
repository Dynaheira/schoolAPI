using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebApp.DTO;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
    public class GradesController : Controller {
        private GradeService _gradeService;

        public GradesController(GradeService gradeService) {
            _gradeService = gradeService;
        }

        public async Task<IActionResult> CreateAsync() {
            await FillSelectsAsync();
            return View();
        }

        private async Task FillSelectsAsync() {
            var gradesDropdownsData = await _gradeService.GetGradesDropdownsData();
            ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> Create(GradeDTO gradeDTO) {
            await _gradeService.CreateAsync(gradeDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index() {
            var allGrades = await _gradeService.GetAllVMsAsync();
            return View(allGrades);

        }

        public async Task<IActionResult> Update(int id) {
            var gradeToEdit = await _gradeService.GetByIdAsync(id);
            if (gradeToEdit == null) {
                return View("NotFound");
            }
            var gradeDTO = _gradeService.ModelToDTO(gradeToEdit);
            await FillSelectsAsync();
            return View(gradeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update (int id, GradeDTO gradeDTO) {
            await _gradeService.UpdateAsync(id, gradeDTO);
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult>
            DeleteAsync(int id) {
            await _gradeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
