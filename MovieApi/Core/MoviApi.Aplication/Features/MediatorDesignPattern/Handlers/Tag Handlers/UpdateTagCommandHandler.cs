﻿
namespace MovieApi.Application.Features.MediatorDesignPattern.Handlers.Tag_Handlers
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly MovieContext _context;

        public UpdateTagCommandHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Tags.FindAsync(request.TagId);
            values.Title = request.Title;
            await _context.SaveChangesAsync();
        }
    }
}
