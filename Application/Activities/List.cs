using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class ListQuery : IRequest<List<Activity>>
        {

        }

        public class ListQueryHandler : IRequestHandler<ListQuery, List<Activity>>
        {
            private readonly DataContext _context;
            //private readonly ILogger<List> _logger;
            public ListQueryHandler(DataContext context)
            //public Handler(DataContext context, ILogger<List> logger)
            {
                //_logger = logger;
                _context = context;

            }
            public async Task<List<Activity>> Handle(ListQuery request, CancellationToken cancellationToken)
            {
                /*
                try
                {
                    for (var i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        _logger.LogInformation($"Task {i} has completed");
                    }
                }
                catch (Exception ex) when (ex is TaskCanceledException)
                {
                    _logger.LogInformation("Task was canceled");
                }
                */
                var activities = await _context.Activities.ToListAsync();
                //var activities = await _context.Activities.ToListAsync(cancellationToken);
                return activities;
            }
        }
    }
}