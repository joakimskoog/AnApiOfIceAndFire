using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace AnApiOfIceAndFire.Infrastructure
{
    public class ApplicationInsightExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            if (context != null && context.Exception != null)
            {
                var ai = new TelemetryClient();
                ai.TrackException(context.Exception);
            }

            base.Log(context);
        }
    }
}