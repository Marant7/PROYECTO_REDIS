using Microsoft.AspNetCore.Mvc;
using RedisLogin.Models;
using RedisLogin.Services;
using Microsoft.Extensions.Configuration;

namespace RedisLogin.Controllers
{
    public class AccountController : Controller
    {
        private readonly RedisService _redisService;

        public AccountController(RedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_redisService.ValidateUser(model.Username, model.Password))
            {
                string sessionId = Guid.NewGuid().ToString();
                _redisService.SaveUserSession(model.Username, sessionId);
                
                Response.Cookies.Append("SessionId", sessionId, new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1)
                });

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return View(model);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("SessionId");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_redisService.RegisterUser(model.Username, model.Password))
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "El usuario ya existe");
            return View(model);
        }
    }
}