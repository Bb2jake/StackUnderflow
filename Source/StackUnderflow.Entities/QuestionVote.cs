using System;

namespace StackUnderflow.Entities
{
	public class QuestionVote
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public int QuestionId { get; set; }

		public bool Upvote { get; set; }
	}
}
