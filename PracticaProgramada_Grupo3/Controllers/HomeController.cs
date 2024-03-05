using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada_Grupo3.Models;
using System.Diagnostics;

namespace PracticaProgramada_Grupo3.Controllers
{
    public class HomeController : Controller
    {
        private readonly Tarea02Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, Tarea02Context context)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PrivacyAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId.HasValue)
            {
                var games = await _context.Games.Where(g => g.UserId == userId.Value).ToListAsync();
                return View(games);
            }
            else
            {
                
                return View(new List<Game>());
            }
        }

        public IActionResult Inicio()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
