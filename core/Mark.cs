using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class Mark
{
    public int MarkId { get; set; }

    public string Mark1 { get; set; } = null!;

    public int MinimumLimit { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
