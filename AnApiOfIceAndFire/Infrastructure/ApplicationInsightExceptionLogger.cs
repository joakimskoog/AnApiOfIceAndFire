using System;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace AnApiOfIceAndFire.Infrastructure
{
    public class ApplicationInsightExceptionLogger : ExceptionLogger
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightExceptionLogger(TelemetryClient telemetryClient)
        {
            if (telemetryClient == null) throw new ArgumentNullException(nameof(telemetryClient));
            _telemetryClient = telemetryClient;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            if (context != null && context.Exception != null)
            {
                _telemetryClient.TrackException(context.Exception);
            }

            base.Log(context);
        }
    }
}