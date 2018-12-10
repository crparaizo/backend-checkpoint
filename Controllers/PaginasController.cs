using Microsoft.AspNetCore.Mvc;

namespace Senai.Checkpoint.Mvc.Controllers {
    public class PaginasController : Controller {

        [HttpGet]
        public IActionResult Home () {
            return View ();
        }

        [HttpGet]
        public IActionResult Empresa () {
            return View ();
        }

        [HttpGet]
        public IActionResult Faq () {
            return View ();
        }

        [HttpGet]
        public IActionResult Contato () {
            return View ();
        }
    }
}