using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class Session
{
    public int SessionId { get; set; }

    public int TestId { get; set; }

    public DateTime SessionDate { get; set; }

    public TimeSpan Duration { get; set; }

    public bool IsSuccessful { get; set; }

    public string? FailureReason { get; set; }

    public int Score { get; set; }

    public int MaxScore { get; set; }

    public int? CorrectPercent { get; set; }

    public int? MarkId { get; set; }

    public int UserUd { get; set; }

    public virtual Mark? Mark { get; set; }

    public virtual Test Test { get; set; } = null!;
}
