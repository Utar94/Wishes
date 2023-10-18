namespace Logitar.Wishes.Domain.Exceptions;

public interface IExceptionMessageBuilder
{
  IExceptionMessageBuilder AddData(object key, object? value);

  string Build();
}
