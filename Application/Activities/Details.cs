using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class DetailsQuery : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }

        public class DetailsQueryHandler : IRequestHandler<DetailsQuery, Activity>
        {
            private readonly DataContext _context;
            public DetailsQueryHandler(DataContext context)
            {
                _context = context;

            }
            public async Task<Activity> Handle(DetailsQuery request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                return activity;
            }
        }
    }
}