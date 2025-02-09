using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TestSubmissionDto
    {
        public Guid UserId { get; set; }
        public Guid TestId { get; set; }
        public List<SubmittedAnswerDto> Answers { get; set; }
    }

    public class SubmittedAnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}