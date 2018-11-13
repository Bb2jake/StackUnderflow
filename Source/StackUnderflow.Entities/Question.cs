using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackUnderflow.Entities
{
	public class Question
	{
		public int Id { get; set; }

		public string Body { get; set; }

		public int CreatedBy { get; set; }

		public DateTimeOffset CreatedDate { get; set; }

		public int? AcceptedAnswerId { get; set; }

		[NotMapped]
		public int Votes { get; set; }
	}
}
