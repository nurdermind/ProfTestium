using System;
using System.Collections.Generic;

namespace ProfTestium_TestService;

public partial class Question
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int QuestionTypeId { get; set; }

    public int MaxScore { get; set; }

    public int? PictureId { get; set; }

    public virtual ICollection<AnswerVariant> AnswerVariants { get; set; } = new List<AnswerVariant>();

    public virtual ICollection<BankOfQuestion> BankOfQuestions { get; set; } = new List<BankOfQuestion>();

    public virtual ICollection<OpenQuestionAnswer> OpenQuestionAnswers { get; set; } = new List<OpenQuestionAnswer>();

    public virtual Picture? Picture { get; set; }

    public virtual QuestionType QuestionType { get; set; } = null!;
}
