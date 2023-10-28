using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class BankOfQuestion
{
    public int PositionId { get; set; }

    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
