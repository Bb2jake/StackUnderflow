using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Data;
using StackUnderflow.Entities;
using System.Threading.Tasks;
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
		public IActionResult PostAnswer([FromBody] Answer answer)
		{
			if (!ModelState.IsValid) return BadRequest();

			answer.CreatedBy = "123";
			_as.PostAnswer(answer);
			return Ok(answer);
		}


		[HttpPut("{answerId}")]
		// GET: Questions/Edit/5
		public IActionResult VoteOnAnswer(int answerId, [FromQuery] bool upvote)
		{
//			var userId = _um.GetUserId(HttpContext.User);

			_as.Vote("123", answerId, upvote);
			return Ok();
		}
	}
}
