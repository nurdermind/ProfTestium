using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using ProfTestium_TestService;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ProfTestium_TestService.Services
{
    public class SessionSenderService : SessionSender.SessionSenderBase
    {
        public override Task<SessionSenderReply> GetSessionList(SessionSenderRequest request, ServerCallContext context)
        {
            using (CoreContext datacontext = new CoreContext())
            {
                List<Session> sessionsOfUser = datacontext.Sessions.Include(f=>f.Test).Include(a=>a.Mark).ToList().Where(f => f.UserUd == request.UserID).ToList();
                SessionSenderReply reply = new SessionSenderReply();
                var sessionsFormat = sessionsOfUser.Select(item => new SessionUser
                {
                    SessionID = item.SessionId,
                    TestssenderReply = new TestsSenderReply { TestID = item.Test.TestId, TestName = item.Test.Testname },
                    Durations = Duration.FromTimeSpan(item.Duration),
                    SessionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(TimeZoneInfo.ConvertTimeToUtc(item.SessionDate)),
                    FailureReason = item.FailureReason,
                    IsSuccessful = item.IsSuccessful,
                    MaxScore = item.MaxScore,
                    Score = item.Score
                }); 
                reply.Sessions.AddRange(sessionsFormat);
                return Task.FromResult(reply);
            }
        }
    }
}