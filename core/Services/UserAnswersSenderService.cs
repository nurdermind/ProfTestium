using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ProfTestium_TestService.Services
{
    public class UserAnswersSenderService:UserAnswersSender.UserAnswersSenderBase
    {
        public override Task<GetUserAnswerReply> GetUserAnswers(GetUserAnswersRequest request, ServerCallContext context)
        {
            using(CoreContext datacontext = new CoreContext())
            {
                Question question = datacontext.Questions.Include(f=>f.Picture).Include(f=>f.AnswerVariants).ThenInclude(f=>f.UserAnswers).FirstOrDefault(f => f.QuestionId == request.QuestionID);
                AnswerVariants answerVariants = new AnswerVariants();
                var ansawersvarFormat = question.AnswerVariants.Select(item => new AnswerVariants
                {
                    IsCorrect = item.IsCorrect,
                    VariantID = item.VariantId,
                    VariantText = item.VariantText
                });
                
                GetUserAnswerReply reply = new GetUserAnswerReply
                {
                    Question = new UserQuestionReply
                    {
                        QuestionID = request.QuestionID,
                        QuestionText = question.QuestionText,
                        MaxScore = question.MaxScore,
                        PictureID = question.PictureId,
                        PicturePath = question.Picture.PicturePath,
                    }
                };
                foreach (var answer in question.AnswerVariants)
                {
                    if (answer.UserAnswers.Any(f=>f.UserId==request.UserID))
                    {
                        reply.Question.UserAsnwerVariantID = answer.VariantId;
                    }
                }
                reply.Question.AnswersVar.AddRange(ansawersvarFormat);
                return Task.FromResult(reply);
            }
        }
    }
}
