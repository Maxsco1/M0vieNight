using System.Net;

namespace MovieNight.Domain.Exceptions;

public sealed class EntityDoesNotExistException(string entityType, string fieldName, object fieldValue) : BaseMovieNightException($"No {entityType} with {fieldName} \"{fieldValue}\" exists.", HttpStatusCode.NotFound)
{
}