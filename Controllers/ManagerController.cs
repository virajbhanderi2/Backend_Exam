using Microsoft.AspNetCore.Mvc;

namespace Backend_Exam.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
