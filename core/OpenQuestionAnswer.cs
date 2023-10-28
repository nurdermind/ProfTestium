using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class OpenQuestionAnswer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public string? UserAnswerText { get; set; }

    public string CorrectAnswerText { get; set; } = null!;

    public int Score { get; set; }

    public int UserId { get; set; }

    public virtual Question Question { get; set; } = null!;
}
