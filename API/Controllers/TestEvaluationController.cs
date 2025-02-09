using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API.Controllers
{
    public class TestEvaluationController : BaseApiController
    {
        private readonly DataContext _context;

        public TestEvaluationController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> EvaluateTest([FromBody] TestSubmissionDto submission)
        {
            var test = await _context.Tests.Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(t => t.Id == submission.TestId);
            if (test == null) return NotFound();

            int correctAnswers = 0;
            foreach (var submittedAnswer in submission.Answers)
            {
                var question = test.Questions.FirstOrDefault(q => q.Id == submittedAnswer.QuestionId);
                if (question != null && question.Answers.Any(a => a.Id == submittedAnswer.AnswerId && a.IsCorrect))
                {
                    correctAnswers++;
                }
            }

            var result = new TestResult
            {
                UserId = submission.UserId,
                TestId = submission.TestId,
                Score = (double)correctAnswers / test.Questions.Count * 100,
            };
            _context.TestResults.Add(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }

}