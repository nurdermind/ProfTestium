using Grpc.Core;
using Microsoft.EntityFrameworkCore;
namespace ProfTestium_TestService.Services
{
    public class DetailTestSenderService : DetailTestSender.DetailTestSenderBase
    {
        public override Task<DetailTestSenderReply> GetDetailTest(DetailListSenderRequest request, ServerCallContext context)
        {
            using(CoreContext datacontext= new CoreContext())
            {
                Test tests = datacontext.Tests.Include(f=>f.BankOfQuestions).FirstOrDefault(f=>f.TestId==request.TestID);
                List<BankOfQuestion> bankQuestions = datacontext.BankOfQuestions.Include(f=>f.Question).Where(f=> f.TestId == request.TestID).ToList();
                DetailTestSenderReply bankReply = new DetailTestSenderReply();
                //if(null)
                var bankFormat = bankQuestions.Select(item => new BankOfQuestionReply
                {
                    PositionID = item.PositionId,
                    QuestionID = item.QuestionId,
                    Question = new QuestionReply
                    {
                        MaxScore = item.Question.MaxScore,
                        PictureID = item.Question.PictureId,
                        PicturePath = datacontext.Pictures.FirstOrDefault(f=>f.PictureId== item.Question.PictureId).PicturePath,
                        QuestionText = item.Question.QuestionText,
                        QuestionTypeID = item.Question.QuestionTypeId
                    }
                });;
                bankReply.BankOfQuestions.AddRange(bankFormat);
                DetailTestSenderReply detailTestSender = new DetailTestSenderReply();
                detailTestSender.BankOfQuestions.AddRange(bankFormat);
                detailTestSender.TestID = request.TestID;
                detailTestSender.TestName = tests.Testname;
                return Task.FromResult(detailTestSender);
            }
        }
    }
}
