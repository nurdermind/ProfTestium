using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ProfTestium_TestService.Services
{
    public class DetailTestListSenderService : DetailTestsListSender.DetailTestsListSenderBase
    {
        public override Task<GetQuestionsListReply> GetQuestionsList(GetQuestionsListRequest request, ServerCallContext context)
        {
            using(CoreContext datacontext = new CoreContext()) { 

                List<Question> questions = datacontext.Questions.Include(f=>f.QuestionType).Include(f=>f.Picture).Include(f=>f.BankOfQuestions).Where(f=>f.BankOfQuestions.Any(s=>s.TestId==request.TestID)).ToList();//?
                GetQuestionsListReply reply = new GetQuestionsListReply();
                var questionsFormat = questions.Select(item => new QuestionElementReply
                {
                    QuestionID = item.QuestionId,
                    QuestionText = item.QuestionText,
                    MaxScore = item.MaxScore,
                    TypeQuestion = new QuestionsTypeReply
                    {
                        TypeID = item.QuestionType.QuestionTypeId,
                        TypeName = item.QuestionType.QuestionTypeName,
                    },
                    Picture = new PictureReply
                    {
                        PathPicture = item.Picture.PicturePath,
                        PictureID = item.Picture.PictureId,
                    },
                });
                reply.Questions.AddRange(questionsFormat);
                return Task.FromResult(reply);
            }
        }
        public override Task<GetOpenQuestionAnswerReply> GetOpenQuestionAnswer(GetOpenQuestionAnswersOfUserRequest request, ServerCallContext context)
        {
            using (CoreContext datacontext = new CoreContext())
            {
                List<OpenQuestionAnswer> openQuestionAnswers = datacontext.OpenQuestionAnswers.Where(f => f.UserId == request.UserID).ToList();
                GetOpenQuestionAnswerReply reply = new GetOpenQuestionAnswerReply();
                var openQuestionAnswersFormat = openQuestionAnswers.Select(item => new OpenQuestionAnswerReply
                {
                    AnswerID = item.AnswerId,
                    CorrectAnswer = item.CorrectAnswerText,
                    QuestionID = item.QuestionId,
                    Score = item.Score,
                    UserAnswerText = item.UserAnswerText,
                    UserID = request.UserID,
                });
                reply.OpenQuestions.AddRange(openQuestionAnswersFormat);
                return Task.FromResult(reply);
            }
        }
        public override Task<GetCloseVariantsQuestionAnswerReply> GetCloseQuestionAnswer(GetVariantsAnswareOfCloseTypeQuestion request, ServerCallContext context)
        {
            using (CoreContext datacontext = new CoreContext())
            {
                List<AnswerVariant> answerVariants = datacontext.AnswerVariants.Where(f=>f.QuestionId == request.QuestionID).ToList();
                GetCloseVariantsQuestionAnswerReply reply = new GetCloseVariantsQuestionAnswerReply();
                var answerVariantsFormat = answerVariants.Select(item => new CloseQuestionAnswerReply
                {
                    CodeofAnswer = item.VariantId,
                    IsCorrect = item.IsCorrect,
                    QuestionID= item.QuestionId,
                    TextVariant = item.VariantText,
                });
                reply.CloseQuestions.AddRange(answerVariantsFormat);
                return Task.FromResult(reply);
            }
        }

    }
}
