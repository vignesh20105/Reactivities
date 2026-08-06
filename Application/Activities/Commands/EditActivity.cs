using System.Diagnostics;
using MediatR;
using Persistence;
using Domain;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Domain.Activity Activity { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken)
            ?? throw new Exception("Cannot find Activity");

            activity.Title = request.Activity.Title;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}