﻿using System;

namespace StackUnderflow.Entities
{
	public class AnswerVote
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public int AnswerId { get; set; }

		public bool Upvote { get; set; }
	}
}
