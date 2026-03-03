using System.Net;

namespace EnergiApp.Application.Exceptions
{
    using System;
    using System.Net;

    public class NordPoolApiException : Exception
    {
        public NordPoolApiException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public NordPoolApiException(string message, Exception innerException, HttpStatusCode httpStatusCode) : base(
            message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}


