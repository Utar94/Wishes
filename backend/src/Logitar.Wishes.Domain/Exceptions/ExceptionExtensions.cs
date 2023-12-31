﻿namespace Logitar.Wishes.Domain.Exceptions;

public static class ExceptionExtensions
{
  public static string GetErrorCode(this Exception exception) => exception.GetType().Name.Remove(nameof(Exception));
}
