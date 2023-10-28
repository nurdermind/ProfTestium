using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
namespace ProfTestium_TestService.Services
{
    public class TestsListSenderService:TestsListSender.TestsListSenderBase
    {
        public override Task<TestsListSenderReply> GetTestsList(TestsListSenderRequest request, ServerCallContext context)
        {
            using(CoreContext datacontext = new CoreContext())
            {
                List<Test> testList = datacontext.Tests.ToList();
                TestsListSenderReply reply = new TestsListSenderReply();
                var testListFormat = testList.Select(item => new TestReply
                {
                    TestID = item.TestId,
                    TestName = item.Testname,
                });
                reply.Tests.AddRange(testListFormat);
                return Task.FromResult(reply);
            }
        }
    }
}
