using FluentResults;
using GamesBack.Domain.GameAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GamesBack.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _ = next ?? throw new ArgumentNullException(nameof(next));

        var result = await next();
        if (result.IsFailed)
        {
            _logger.LogError("Request failure {Name}, Errors: {@Errors}", typeof(TRequest).Name, result.Errors);
        }

        return result;
    }
}