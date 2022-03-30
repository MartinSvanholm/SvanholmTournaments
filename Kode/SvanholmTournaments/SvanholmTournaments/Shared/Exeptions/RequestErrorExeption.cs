using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.Exeptions;

public class RequestErrorExeption : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public RequestErrorExeption(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
