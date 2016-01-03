using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AnApiOfIceAndFire.Results
{
    public abstract class ChainedResult : IHttpActionResult
    {
        private readonly IHttpActionResult _nextActionResult;

        protected ChainedResult(IHttpActionResult nextActionResult)
        {
            if (nextActionResult == null) throw new ArgumentNullException(nameof(nextActionResult));
            _nextActionResult = nextActionResult;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return _nextActionResult.ExecuteAsync(cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    ApplyActionResult(response);
                    return response;

                }, cancellationToken);
        }

        protected abstract void ApplyActionResult(HttpResponseMessage response);
    }
}