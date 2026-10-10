using System.Net;

namespace MovieNight.Domain.Exceptions;

public abstract class BaseMovieNightException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : Exception(message)
{
    public HttpStatusCode Status { get; set; } = statusCode;
}