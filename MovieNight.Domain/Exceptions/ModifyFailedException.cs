using System.Net;
namespace MovieNight.Domain.Exceptions;

public sealed class ModifyFailedException(string action, string entityType)
: BaseMovieNightException($"Failed to {action} any {entityType}s.", HttpStatusCode.InternalServerError)
{
}