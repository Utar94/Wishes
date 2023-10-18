using FluentValidation.Results;
using Logitar.EventSourcing;
using Logitar.Wishes.Domain.Exceptions;

namespace Logitar.Wishes.Application.Exceptions;

public class AggregateNotFoundException : Exception, IFailureException
{
  private const string ErrorMessage = "The specified aggregate could not be found.";

  public Type Type
  {
    get => Type.GetType((string)Data[nameof(Type)]!) ?? throw new InvalidOperationException("The type could not be resolved.");
    private set => Data[nameof(Type)] = value.GetName();
  }
  public AggregateId Identifier
  {
    get => new((string)Data[nameof(Identifier)]!);
    private set => Data[nameof(Identifier)] = value.Value;
  }
  public string PropertyName
  {
    get => (string)Data[nameof(PropertyName)]!;
    private set => Data[nameof(PropertyName)] = value;
  }

  public ValidationFailure Failure => new(PropertyName, ErrorMessage, Identifier.Value)
  {
    ErrorCode = this.GetErrorCode()
  };

  public AggregateNotFoundException(Type type, AggregateId identifier, string propertyName)
    : base(BuildMessage(type, identifier, propertyName))
  {
    if (!type.IsSubclassOf(typeof(AggregateRoot)))
    {
      throw new ArgumentException($"The type must be a subclass of {nameof(AggregateRoot)}.", nameof(type));
    }

    Type = type;
    Identifier = identifier;
    PropertyName = propertyName;
  }

  private static string BuildMessage(Type type, AggregateId identifier, string propertyName) => new ExceptionMessageBuilder(ErrorMessage)
    .AddData(nameof(Type), type.GetName())
    .AddData(nameof(Identifier), identifier.Value)
    .AddData(nameof(PropertyName), propertyName)
    .Build();
}

public class AggregateNotFoundException<T> : AggregateNotFoundException where T : AggregateRoot
{
  public AggregateNotFoundException(AggregateId identifier, string propertyName) : base(typeof(T), identifier, propertyName)
  {
  }
}
