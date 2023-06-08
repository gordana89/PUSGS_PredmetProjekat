using System;

namespace UsersMicroservice.Api.Exceptions
{
    public class BadRequestException : Exception
    {
        public int StatusCode { get; set; }

        public BadRequestException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
