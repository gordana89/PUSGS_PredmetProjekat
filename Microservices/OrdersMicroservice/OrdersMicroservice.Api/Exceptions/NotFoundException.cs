using System;

namespace OrdersMicroservice.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; set; }

        public NotFoundException(string message) : base(message)
        {
            StatusCode = 500;
        }
    }
}
