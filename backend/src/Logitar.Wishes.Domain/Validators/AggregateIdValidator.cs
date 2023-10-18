using FluentValidation;
using Logitar.EventSourcing;
using Logitar.Wishes.Domain.Extensions;

namespace Logitar.Wishes.Domain.Validators;

public class AggregateIdValidator : AbstractValidator<string>
{
  public AggregateIdValidator(string? propertyName = null)
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(AggregateId.MaximumLength)
      .WithPropertyName(propertyName);
  }
}
