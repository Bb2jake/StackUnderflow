using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Data;
using StackUnderflow.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StackUnderflow.Business;
using StackUnderflow.Entities.DTOs;

namespace StackUnderflow.Web.Controllers
{
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] UserLoginDto userLoginDto)
		{
			var result = _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, true, false).Result;
			if (result.Succeeded) return Ok();
			return BadRequest();
		}

		[HttpPost("register")]
		// GET: Questions/Edit/5
		public IActionResult Register([FromBody] UserLoginDto userLoginDto)
		{
			var result = _userManager.CreateAsync(new ApplicationUser {UserName = userLoginDto.UserName}, userLoginDto.Password).Result;
			if (result.Succeeded) return Ok();
			return BadRequest();
		}

		[HttpDelete("logout")]
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync().Wait();
			return Ok();
		}
	}
}
