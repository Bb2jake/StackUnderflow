using System;

namespace StackUnderflow.Entities
{
	public class Comment
	{
		public int Id { get; set; }

		public int AnswerId { get; set; }

		public string Body { get; set; }

		public string CreatedBy { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
	}
}
