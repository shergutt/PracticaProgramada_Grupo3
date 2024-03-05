using Microsoft.AspNetCore.Mvc;

namespace PracticaProgramada_Grupo3.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
