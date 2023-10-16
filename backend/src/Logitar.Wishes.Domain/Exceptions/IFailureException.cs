using FluentValidation.Results;

namespace Logitar.Wishes.Domain.Exceptions;

public interface IFailureException
{
  ValidationFailure Failure { get; }
}
