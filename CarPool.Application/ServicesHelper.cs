using System;
using CarPool.Common;
using Microsoft.Extensions.Logging;

namespace CarPool.Application;

/// <summary>
/// Service result helper. See <see cref="IResult"/>, <see cref="IResult{T}"/>
/// </summary>
public static class ServicesHelper
{
    public static void HandleServiceError<T, P>(ref Result<T> serviceResult, ILogger<P> logger, Exception ex, string uiErrorMessage)
    {
        logger.LogError($"{nameof(HandleServiceError)}: {ex}");
        if (serviceResult.Errors.Any())
        {
            logger.LogError($"Result errors: {string.Join(Environment.NewLine, serviceResult.Errors)}");
        }
        serviceResult
            .Failed()
            .WithMessage(uiErrorMessage)
            .WithException(ex)
            .WithData(default);
    }

    public static void HandleServiceError<P>(ref Result serviceResult, ILogger<P> logger, Exception ex, string uiErrorMessage)
    {
        logger.LogError($"{nameof(HandleServiceError)}: {ex}");
        if (serviceResult.Errors.Any())
        {
            logger.LogError($"Result errors: {string.Join(Environment.NewLine, serviceResult.Errors)}");
        }
        serviceResult
            .Failed()
            .WithException(ex)
            .WithMessage(uiErrorMessage);
    }
}

