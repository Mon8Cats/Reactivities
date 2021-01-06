using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;
using System.Net;

namespace Application.Activities
{
    public class Delete
    {
        public class DeleteCommand : IRequest
        {
            public Guid Id { get; set; }
        }

        public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
        {
            private readonly DataContext _context;
            public DeleteCommandHandler(DataContext context)
            {
                _context = context;

            }
            public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound,
                        new { activity = "Could not find activity" });

                _context.Remove(activity);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}