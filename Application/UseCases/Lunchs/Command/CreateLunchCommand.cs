﻿using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Lunchs.Command
{

    public class CreateLunchCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }

    public class CreateLunchCommandHandler : IRequestHandler<CreateLunchCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        public CreateLunchCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateLunchCommand request, CancellationToken cancellationToken)
        {
            var lunch = new Lunch
            {
                Name = request.Name,
                Img = request.Img,
                Price = request.Price,
                Rewievs = request.Rewievs,
                Quality = request.Quality
            };

            var entity = await _context.Lunchs.AddAsync(lunch);
            await _context.SaveChangesAsync();

            return lunch.Id;
        }
    }
}