using StackUnderflow.Data;
using StackUnderflow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackUnderflow.Business
{
	public class AnswerService
	{
		private readonly ApplicationDbContext _context;

		public AnswerService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Answer> GetAnswers(int questionId)
		{
			try
			{
				return _context.Answers.Where(a => a.QuestionId == questionId).ToList();
			}
			catch
			{
				throw new Exception();
			}
		}
		public void PostAnswer(Answer answer)
		{
			try
			{
				// Make sure the question exists
				var question = _context.Questions.Find(answer.QuestionId);
				if (question == null) throw new Exception("Question not found");

				answer.CreatedDate = DateTimeOffset.Now;
				_context.Answers.Add(answer);
				_context.SaveChanges();
			}
			catch
			{
				throw new Exception();
			}
		}

		public void Vote(string userId, int answerId, bool upVote)
		{
			try
			{
				var existingVote = _context.AnswerVotes.FirstOrDefault(v => v.UserId == userId && v.AnswerId == answerId);

				// if vote doesn't exist, add it
				if (existingVote == null)
				{
					_context.AnswerVotes.Add(new AnswerVote()
					{
						UserId = userId,
						AnswerId = answerId,
						Upvote = upVote
					});

				}
				// TODO: Add a way to remove the vote if they upvote an upvote or downvote a downvote
				// if vote exists, update it
				else
				{
					existingVote.Upvote = upVote;
				}

				_context.SaveChanges();
			}
			catch
			{
				throw new Exception();
			}
		}

	}
}
