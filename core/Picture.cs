using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class Picture
{
    public int PictureId { get; set; }

    public string PicturePath { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
