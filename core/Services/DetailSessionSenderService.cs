using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ProfTestium_TestService.Services
{
    public class DetailSessionSenderService : DetailSessionSender.DetailSessionSenderBase
    {
        public override Task<DetailSessionSenderReply> GetDetailSession(DetailSessionSenderRequest request, ServerCallContext context)
        {
            using (CoreContext datacontext = new CoreContext())
            {
                Session? session = datacontext.Sessions.Include(f => f.Test).Include(a => a.Mark).FirstOrDefault(f => f.SessionId == request.SessionID);
                if(session == null)
                {
                    DetailSessionUser detailSessionUser = new DetailSessionUser()
                    {
                        Score = session.Score,
                        SessionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(TimeZoneInfo.ConvertTimeToUtc(session.SessionDate)),
                        Durations = Duration.FromTimeSpan(session.Duration),
                        SessionID = request.SessionID,
                        IsSuccessful = session.IsSuccessful,
                        MaxScore = session.MaxScore,
                        FailureReason = session.FailureReason,
                        TestsenderReply = new DetailTestsSenderReply { TestID = session.Test.TestId, TestName = session.Test.Testname },
                    };
                    return Task.FromResult(new DetailSessionSenderReply { Session = detailSessionUser });
                }
                return Task.FromResult(new DetailSessionSenderReply { Session = null });
            }
        }
    }
}
