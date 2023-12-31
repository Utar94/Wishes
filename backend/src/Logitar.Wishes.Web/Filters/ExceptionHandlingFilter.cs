﻿using FluentValidation;
using Logitar.Wishes.Application.Exceptions;
using Logitar.Wishes.Application.Wishlists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logitar.Wishes.Web.Filters;

internal class ExceptionHandlingFilter : ExceptionFilterAttribute
{
  private static readonly Dictionary<Type, Func<ExceptionContext, IActionResult>> _handlers = new()
  {
    [typeof(ItemNotFoundException)] = HandleItemNotFoundException,
    [typeof(ValidationException)] = HandleValidationException
  };

  public override void OnException(ExceptionContext context)
  {
    if (_handlers.TryGetValue(context.Exception.GetType(), out Func<ExceptionContext, IActionResult>? handler))
    {
      context.Result = handler(context);
      context.ExceptionHandled = true;
    }
    else if (context.Exception is AggregateNotFoundException aggregateNotFound)
    {
      context.Result = new NotFoundObjectResult(aggregateNotFound.Failure);
      context.ExceptionHandled = true;
    }
    else if (context.Exception is IdentifierAlreadyUsedException identifierAlreadyUsed)
    {
      context.Result = new ConflictObjectResult(identifierAlreadyUsed.Failure);
      context.ExceptionHandled = true;
    }
  }

  private static IActionResult HandleItemNotFoundException(ExceptionContext context)
  {
    return new NotFoundObjectResult(((ItemNotFoundException)context.Exception).Failure);
  }

  private static IActionResult HandleValidationException(ExceptionContext context)
  {
    return new BadRequestObjectResult(new { ((ValidationException)context.Exception).Errors });
  }
}
