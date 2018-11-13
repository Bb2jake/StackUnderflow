using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Data;
using StackUnderflow.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StackUnderflow.Business;

namespace StackUnderflow.Web.Controllers
{
	public class AnswersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly AnswerService _as;
		private readonly UserManager<IdentityUser> _um;

		public AnswersController(UserManager<IdentityUser> um, ApplicationDbContext context, AnswerService answerService)
		{
			_context = context;
			_as = answerService;
			_um = um;
		}

		// GET: Answers/questionId
		[HttpGet("{questionId}")]
		public IActionResult GetQuestionAnswers(int questionId)
		{
			var answers = _as.GetAnswers(questionId);
			return Ok(answers);
		}

		// POST: Answers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> PostAnswer([Bind("Id,QuestionId,Body,CreatedBy,CreatedDate")] Answer answer)
		{
			if (!ModelState.IsValid) return BadRequest();

			var userId = _um.GetUserId(HttpContext.User);
			answer.CreatedBy = userId;
			_as.PostAnswer(answer);
			return Ok(answer);

		}


		[HttpPut("{answerId}")]
		[ValidateAntiForgeryToken]
		// GET: Questions/Edit/5
		public async Task<IActionResult> VoteOnAnswer(int answerId, [FromBody] bool upvote)
		{
			var userId = _um.GetUserId(HttpContext.User);

			_as.Vote(userId, answerId, upvote);
			return Ok();
		}
	}
}
