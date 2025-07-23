using Swashbuckle.AspNetCore.Filters;

namespace PersonManagement.Api.Examples.Responses.ErrorResponses
{
    public class AlreadyExistsErrorResponseExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse { ErrorMessage = "Already exists." };
        }
    }
}
