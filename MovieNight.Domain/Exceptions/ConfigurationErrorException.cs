namespace MovieNight.Domain.Exceptions;

public class ConfigurationErrorException : Exception {
  public ConfigurationErrorException() {}
  public ConfigurationErrorException(string message): base(message) {}
  public ConfigurationErrorException(string message, Exception innerException) : base(message, innerException) {}
}