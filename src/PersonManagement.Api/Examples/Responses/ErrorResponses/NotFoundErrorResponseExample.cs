using Swashbuckle.AspNetCore.Filters;

namespace PersonManagement.Api.Examples.Responses.ErrorResponses
{
    public class NotFoundErrorResponseExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse { ErrorMessage = "Not found." };
        }
    }
}
