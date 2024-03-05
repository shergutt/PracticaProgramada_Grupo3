using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using PracticaProgramada_Grupo3.Models;

namespace PracticaProgramada_Grupo3.Controllers
{
    public class AccountController : Controller
    {
        private readonly Tarea02Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IWebHostEnvironment hostingEnvironment, Tarea02Context context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            ModelState.Remove("fotoURL");
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == user.Usuario);
                if (existingUser == null)
                {
                    if (user.ProfileImage != null && user.ProfileImage.Length > 0)
                    {
 
                        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + user.ProfileImage.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await user.ProfileImage.CopyToAsync(fileStream);
                        }

                        user.fotoURL = "/images/" + uniqueFileName;
                    }

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario ya existe.");
                }
            }

            return View("Index", user);
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
                    if (!string.IsNullOrWhiteSpace(user.fotoURL))
                    {
                        HttpContext.Session.SetString("FotoURL", user.fotoURL);
                    }
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
