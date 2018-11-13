using StackUnderflow.Data;
using StackUnderflow.Entities;
using System;

namespace StackUnderflow.Business
{
	public class CommentService
	{
		private readonly ApplicationDbContext _context;

		public CommentService(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Create(Comment comment)
		{
			try
			{
				var answer = _context.Answers.Find(comment.AnswerId);
				if (answer == null) throw new Exception("answer not found");
				comment.CreatedDate = DateTimeOffset.Now;

				_context.Comments.Add(comment);
				_context.SaveChanges();
			}
			catch
			{
				throw new Exception();
			}
		}
	}
}
