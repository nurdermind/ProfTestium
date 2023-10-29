using Grpc.Core;

namespace ProfTestium_TestService.Services
{
    public class CreateNewTestOrDelete : CreateDeleteChangeTest.CreateDeleteChangeTestBase
    {
        public override Task<CreateTestReply> CreateTest(CreateTestRequest request, ServerCallContext context)
        {
            using(CoreContext datacontext = new CoreContext())
            {
                Test NewTest = new Test() {Testname = request.TestName};
                datacontext.Add(NewTest);
                datacontext.SaveChanges();
                foreach(var question in request.Questions)
                {
                    if (question.QuestionType == 0)
                    {
                        QuestionType NewQuestionType = datacontext.QuestionTypes.FirstOrDefault(f => f.QuestionTypeId == question.QuestionType);
                        Question NewQuestion = new Question() { QuestionText = question.QuestionText, QuestionType = NewQuestionType, MaxScore = question.MaxScore};
                        datacontext.Questions.Add(NewQuestion);
                        datacontext.SaveChanges();
                        foreach(var variant in question.Answers)
                        {
                            AnswerVariant answerVar = new AnswerVariant() { QuestionId = NewQuestion.QuestionId,IsCorrect = variant.IsCorrect,VariantText = variant.VariantText};
                            datacontext.AnswerVariants.Add(answerVar);
                            datacontext.SaveChanges();
                        }
                        BankOfQuestion bankNote = new BankOfQuestion() { QuestionId = NewQuestion.QuestionId, TestId = NewTest.TestId };
                        datacontext.BankOfQuestions.Add(bankNote);
                        datacontext.SaveChanges();
                    }
                    else
                    {
                        QuestionType NewQuestionType = datacontext.QuestionTypes.FirstOrDefault(f => f.QuestionTypeId == question.QuestionType);
                        Question NewQuestion = new Question() { QuestionText = question.QuestionText, QuestionType = NewQuestionType, MaxScore = question.MaxScore };
                        datacontext.Questions.Add(NewQuestion);
                        datacontext.SaveChanges();
                        BankOfQuestion bankNote = new BankOfQuestion() { QuestionId = NewQuestion.QuestionId, TestId = NewTest.TestId };
                        datacontext.BankOfQuestions.Add(bankNote);
                        datacontext.SaveChanges();
                    }
                }
                return Task.FromResult(new CreateTestReply { Code = 0, IsSuccess = "true" });
            }
        }
    }
}
