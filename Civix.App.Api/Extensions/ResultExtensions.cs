using Civix.App.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Civix.App.Api.Extensions
{
    public static class ResultExtensions
    {
        public static ObjectResult ToProblem(this Result result, int statusCode, string title)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert success result to a problem");
            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Title = title,
                Extensions = new Dictionary<string, object?>
                {
                    {
                        "errors", new [] {result.Error}
                    }
                }
            };


            return new ObjectResult(problemDetails);

        }
    }
}
