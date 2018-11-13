using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Entities.DTOs
{
	public class QuestionDetailDto
	{
		public Question Question { get; set; }
		public List<AnswerDto> AnswerDtos { get; set; }
	}

	public class AnswerDto
	{
		public Answer Answer { get; set; }
		public List<Comment> Comments { get; set; }
	}
}
