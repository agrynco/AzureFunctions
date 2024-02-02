namespace Functions
{
    using System.Net;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.Extensions.Logging;

    public class AddFunction
    {
        private readonly ILogger _logger;

        public AddFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddFunction>();
        }

        [Function("Add")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);

            if (!int.TryParse(query.Get("number1"), out int number1))
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString("Please provide a valid number1");
                return response;
            }
    
            if (!int.TryParse(query.Get("number2"), out int number2))
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString("Please provide a valid number2");
                return response;
            }

            var sum = number1 + number2;

            var okResponse = req.CreateResponse(HttpStatusCode.OK);
            okResponse.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            okResponse.WriteString($"The sum of {number1} and {number2} is {sum}.");

            return okResponse;
        }    }
}
