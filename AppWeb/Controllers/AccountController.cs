using AppWeb.Data;
using AppWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.Controllers
{
	public class AccountController : Controller
	{
		private readonly AppDbContext _context;

		public AccountController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]

		public IActionResult Login (Login model)
		{
			var user = _context.Usuarios.
				FirstOrDefault(u => u.Correo == model.correo && u.Contrasena == model.password);

			if (user == null)
			{
				HttpContext.Session.SetString("usuario", user.Nombre);
				Console.WriteLine("Usuario logueado:" + user.Nombre);
				return RedirectToAction("Index", "Home");
			}
			ViewBag.Error = "Credenciales incorrectas";
			return View();
		}

		public	IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
