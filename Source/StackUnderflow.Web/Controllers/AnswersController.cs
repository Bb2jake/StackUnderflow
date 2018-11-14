using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Data;
using StackUnderflow.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StackUnderflow.Business;

namespace StackUnderflow.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnswersController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly AnswerService _as;

		public AnswersController(ApplicationDbContext context, AnswerService answerService)
		{
			_context = context;
			_as = answerService;
		}

		[HttpPost]
		[Authorize]
		public IActionResult PostAnswer([FromBody] Answer answer)
		{
			if (!ModelState.IsValid) return BadRequest();

			_as.PostAnswer(answer, HttpContext.User.Identity.Name);
			return Ok(answer);
		}


		[HttpPut("{answerId}")]
		[Authorize]
		public IActionResult VoteOnAnswer(int answerId, [FromQuery] bool upvote)
		{
			_as.Vote(HttpContext.User.Identity.Name, answerId, upvote);
			return Ok();
		}
	}
}
