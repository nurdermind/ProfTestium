using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class UserAnswer
{
    public int UserAnswerId { get; set; }

    public int UserId { get; set; }

    public int AnswerVariantId { get; set; }

    public virtual AnswerVariant AnswerVariant { get; set; } = null!;
}
