using Microsoft.AspNetCore.Identity;
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

		// Identity
		public DbSet<ApplicationUser> Users { get; set; }

		public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }

		public DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }

		public DbSet<IdentityRole> IdentityRoles { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityUserRole<string>>().HasKey("RoleId", "UserId");
		}
	}
}
