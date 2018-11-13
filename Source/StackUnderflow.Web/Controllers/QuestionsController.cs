using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Business;
using StackUnderflow.Data;
using StackUnderflow.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
			var question = _qs.GetQuestion(id, "123");

			return Ok(question);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Question question)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_qs.Create(question);

			return Ok(question);
		}

		[HttpPost("{questionId}")]
		// GET: Questions/Edit/5
		public IActionResult VoteOnQuestion(int questionId, [FromQuery] bool upvote)
		{
//			var userId = _um.GetUserId(HttpContext.User);
			_qs.Vote("1", questionId, upvote);
			return Ok();
		}


		[HttpPut("{questionId}/answer/{answerId}")]
		// GET: Questions/Edit/5
		public IActionResult AcceptAnswer(int questionId, int answerId)
		{
			_qs.AcceptAnswer(questionId, answerId);
			return Ok();
		}
	}
}
