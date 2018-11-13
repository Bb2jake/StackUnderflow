using Microsoft.EntityFrameworkCore;
using StackUnderflow.Entities;

namespace StackUnderflow.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Question> Questions { get; set; }

		public DbSet<Answer> Answers { get; set; }

		public DbSet<AnswerVote> AnswerVotes { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<QuestionVote> QuestionVotes { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
