using System.Net;
namespace MovieNight.Domain.Exceptions;

public sealed class ValidationException(IDictionary<string, string[]> errors)
: BaseMovieNightException("One or more validation errors occurred.", HttpStatusCode.BadRequest)
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}