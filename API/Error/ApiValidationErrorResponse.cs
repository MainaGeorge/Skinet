using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.Error
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(StatusCodes.Status400BadRequest)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
