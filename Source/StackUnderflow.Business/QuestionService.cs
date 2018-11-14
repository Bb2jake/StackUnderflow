using StackUnderflow.Data;
using StackUnderflow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.Entities.DTOs;

namespace StackUnderflow.Business
{
	public class QuestionService
	{
		private readonly ApplicationDbContext _context;

		public QuestionService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Question> GetQuestions()
		{
			try
			{
				return _context.Questions
					.GroupJoin(_context.QuestionVotes, q => q.Id, qv => qv.QuestionId, (q, qvs) => new Question
					{
						Id = q.Id,
						Votes = qvs.Count(qv => qv.Upvote) - qvs.Count(qv => !qv.Upvote),
						AcceptedAnswerId = q.AcceptedAnswerId,
						Body = q.Body,
						CreatedBy = q.CreatedBy,
						CreatedDate = q.CreatedDate
					}).ToList();
			}
			catch
			{
				throw new Exception();
			}
		}

		public QuestionDetailDto GetQuestion(int questionId)
		{
			try
			{
				var question = _context.Questions.Find(questionId);
				var votes = _context.QuestionVotes.Where(qv => qv.QuestionId == questionId).ToList();
				question.Votes = votes.Count(qv => qv.Upvote) - votes.Count(qv => !qv.Upvote);

				var answerDtos = _context.Answers.Where(a => a.QuestionId == questionId)
					.GroupJoin(_context.AnswerVotes, a => a.Id, av => av.AnswerId, (a, avs) => new {a, avs = avs.ToList()})
					.GroupJoin(_context.Comments, arg => arg.a.Id, c => c.AnswerId, (arg, comments) => new AnswerDto
					{
						Votes = arg.avs.Count(av => av.Upvote) - arg.avs.Count(av => !av.Upvote),
						Answer = arg.a,
						Comments = comments.ToList()
					})
					.ToList();
				
				return new QuestionDetailDto
				{
					AnswerDtos = answerDtos,
					Question = question
				};
			}
			catch (Exception)
			{
				throw new Exception();
			}
		}

		public Question Create(Question question, string userName)
		{
			try
			{
				question.CreatedDate = DateTimeOffset.Now;
				question.AcceptedAnswerId = null;
				question.CreatedBy = userName;
				_context.Questions.Add(question);
				_context.SaveChanges();
				return question;
			}
			catch
			{
				throw new Exception();
			}
		}

		public void Vote(string userName, int questionId, bool upVote)
		{
			try
			{
				var existingVote = _context.QuestionVotes.FirstOrDefault(v => v.UserName == userName && v.QuestionId == questionId);

				// if vote doesn't exist, add it
				if (existingVote == null)
				{
					_context.QuestionVotes.Add(new QuestionVote()
					{
						UserName = userName,
						QuestionId = questionId,
						Upvote = upVote
					});

				}
				// if vote exists, update it
				else
				{
					existingVote.Upvote = upVote;
					_context.QuestionVotes.Update(existingVote);
				}

				_context.SaveChanges();
			}
			catch
			{
				throw new Exception();
			}
		}

		public void AcceptAnswer(int questionId, int answerId, string userName)
		{
			try
			{
				var question = _context.Questions.Find(questionId);

				if (question == null) throw new Exception("Not found");
				if (question.CreatedBy != userName) throw new Exception("Not the correct user");
				if (question.AcceptedAnswerId != null) throw new Exception("Already accepted an answer!");

				question.AcceptedAnswerId = answerId;

				_context.SaveChanges();
			}
			catch
			{
				throw new Exception();
			}
		}
	}
}
