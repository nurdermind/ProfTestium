using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class Test
{
    public int TestId { get; set; }

    public string? Testname { get; set; }

    public virtual ICollection<BankOfQuestion> BankOfQuestions { get; set; } = new List<BankOfQuestion>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
