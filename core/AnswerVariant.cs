using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class AnswerVariant
{
    public int VariantId { get; set; }

    public string VariantText { get; set; } = null!;

    public int QuestionId { get; set; }

    public bool IsCorrect { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
