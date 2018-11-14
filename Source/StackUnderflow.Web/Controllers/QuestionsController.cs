using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Business;
using StackUnderflow.Data;
using StackUnderflow.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;

namespace StackUnderflow.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionsController : ControllerBase
	{
		private readonly QuestionService _qs;

		public QuestionsController(QuestionService qs)
		{
			_qs = qs;
		}

		// GET: Questions
		[HttpGet]
		public IActionResult GetAllQuestions()
		{
			var questions = _qs.GetQuestions();
			return Ok(questions);
		}

		[HttpGet("{id}")]
		public IActionResult GetQuestionDetails(int id)
		{
			var question = _qs.GetQuestion(id);

			return Ok(question);
		}

		[HttpPost]
		[Authorize]
		public IActionResult Create([FromBody] Question question)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_qs.Create(question, HttpContext.User.Identity.Name);

			return Ok(question);
		}

		[HttpPost("{questionId}")]
		[Authorize]
		public IActionResult VoteOnQuestion(int questionId, [FromQuery] bool upvote)
		{
			_qs.Vote(HttpContext.User.Identity.Name, questionId, upvote);
			return Ok();
		}


		[HttpPut("{questionId}/answer/{answerId}")]
		[Authorize]
		public IActionResult AcceptAnswer(int questionId, int answerId)
		{
			_qs.AcceptAnswer(questionId, answerId, HttpContext.User.Identity.Name);
			return Ok();
		}
	}
}
