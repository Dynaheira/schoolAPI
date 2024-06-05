using Microsoft.AspNetCore.Mvc;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
    public class GradesController : Controller {
        private GradeService _gradeService;

        public GradesController(GradeService gradeService) {
            _gradeService = gradeService;
        }

        public IActionResult Index() {
            return View();
        }
        public IActionResult Create() {
            return View();
        }
    }
}
