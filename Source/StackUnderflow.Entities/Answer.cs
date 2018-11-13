using System;

namespace StackUnderflow.Entities
{
	public class Answer
	{
		public int Id { get; set; }

		public int QuestionId { get; set; }

		public string Body { get; set; }

		public string CreatedBy { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
	}
}
