using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada_Grupo3.Models;

namespace PracticaProgramada_Grupo3.Controllers
{
    public class AccountController : Controller
    {
        private readonly Tarea02Context _context;

        public AccountController(Tarea02Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == user.Usuario);
                if (existingUser == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario ya existe.");
                }
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Usuarios
                                          .FirstOrDefaultAsync(u => u.Usuario == username && u.password == password);
                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserID", user.idUser);
                    HttpContext.Session.SetString("Username", user.Usuario);
                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                }
            }
            return View();
        }

    }
}
