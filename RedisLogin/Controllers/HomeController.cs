using Microsoft.AspNetCore.Mvc;
using RedisLogin.Services;
using Microsoft.Extensions.Configuration;

namespace RedisLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly RedisService _redisService;

        public HomeController(RedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var sessionId = Request.Cookies["SessionId"];
            if (sessionId == null || 
                string.IsNullOrEmpty(_redisService.GetUserFromSession(sessionId)))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}