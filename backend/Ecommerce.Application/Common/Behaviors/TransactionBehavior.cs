using Ecommerce.Application.Interfaces;
using MediatR;

namespace Ecommerce.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IApplicationDbContext _context;

    public TransactionBehavior(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        //TODO
        // if (request is not ICommand)
        //     return await next();
        
        // ICommand.cs
        // public interface ICommand {}
        
        var response = await next();

        await _context.SaveChangesAsync(cancellationToken);

        return response;
    }
}