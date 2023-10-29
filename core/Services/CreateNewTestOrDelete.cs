using Grpc.Core;
using Microsoft.EntityFrameworkCore;

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
        public override Task<DeleteTestReply> DeleteTest(DeleteTestRequest request, ServerCallContext context)
        {
            using (CoreContext datacontext = new CoreContext())
            {
                Test testToDelete = datacontext.Tests.Include(f=>f.Sessions)
                    .Include(f=>f.BankOfQuestions).ThenInclude(f=>f.Question)
                    .ThenInclude(f=>f.AnswerVariants).ThenInclude(f=>f.UserAnswers).Include(f => f.Sessions)
                    .Include(f => f.BankOfQuestions).ThenInclude(f => f.Question).ThenInclude(f=>f.OpenQuestionAnswers)
                    .FirstOrDefault(f=>f.TestId==request.TestID);

                if (testToDelete != null)
                {
                    foreach (var session in testToDelete.Sessions)
                    {
                        datacontext.Sessions.Remove(session);
                    }
                    foreach (var bank in testToDelete.BankOfQuestions)
                    {
                        foreach (var answervar in bank.Question.AnswerVariants)
                        {
                            foreach (var useranwer in answervar.UserAnswers)
                            {
                                datacontext.UserAnswers.Remove(useranwer);
                            }
                            datacontext.AnswerVariants.Remove(answervar);
                        }
                        foreach (var openquestionanswer in bank.Question.OpenQuestionAnswers)
                        {
                            datacontext.OpenQuestionAnswers.Remove(openquestionanswer);
                        }

                        datacontext.BankOfQuestions.Remove(bank);
                        datacontext.Questions.Remove(bank.Question);
                    }
                    datacontext.Tests.Remove(testToDelete);
                    datacontext.SaveChanges();
                    return Task.FromResult(new DeleteTestReply { IsSuccess = "true", Code = 0 });
                }
                else
                {
                    return Task.FromResult(new DeleteTestReply { IsSuccess = "false", Code = 1 });
                }
            }
        }
    }

}
