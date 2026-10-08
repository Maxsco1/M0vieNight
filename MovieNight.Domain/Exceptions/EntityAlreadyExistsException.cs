using System.Net;
namespace MovieNight.Domain.Exceptions;

public sealed class EntityAlreadyExistsException(string entityType, string fieldName, object fieldValue) :
    BaseMovieNightException($"A(n) {entityType} with {fieldName} \"{fieldValue}\" already exists.", HttpStatusCode.Conflict)
{
}