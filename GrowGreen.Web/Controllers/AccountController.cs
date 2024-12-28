using GrowGreen.Application.DTOs;
using GrowGreen.Application.Services;
using GrowGreen.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrowGreen.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository,UserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        // Display login form
        public IActionResult Login()
        {
            return View();
        }

        // Handle login
        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var user = await _userService.AuthenticateUserAsync(userDto.Username, userDto.Password);
            if (user == false)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(userDto);
            }
            var User = _userRepository.GetByUsernameAsync(userDto.Username);
            // Set up session or authentication
            HttpContext.Session.SetString("UserId", User.Id.ToString());
            return RedirectToAction("Index", "Product");
        }

        // Display registration form
        public IActionResult Register()
        {
            return View();
        }

        // Handle registration
        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.RegisterUserAsync(userDto);
                return RedirectToAction(nameof(Login));
            }

            return View(userDto);
        }

        // Handle logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
