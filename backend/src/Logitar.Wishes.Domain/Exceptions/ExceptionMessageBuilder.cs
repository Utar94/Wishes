using System.Text;

namespace Logitar.Wishes.Domain.Exceptions;

public class ExceptionMessageBuilder : IExceptionMessageBuilder
{
  private readonly StringBuilder _message;

  public ExceptionMessageBuilder()
  {
    _message = new();
  }
  public ExceptionMessageBuilder(string message) : this()
  {
    _message.Append(message);
  }

  public IExceptionMessageBuilder AddData(object key, object? value)
  {
    _message.Append(key).Append(": ").Append(value).AppendLine();
    return this;
  }

  public string Build() => _message.ToString();
}
