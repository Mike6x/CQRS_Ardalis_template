using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductsCommand : IRequest<int>
    {
        public List<Product>? Entities { get; set; }
        public class UpdateProductsCommandHandler : IRequestHandler<UpdateProductsCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProductsCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateProductsCommand command, CancellationToken cancellationToken)
            {
                if (command.Entities != null) {
                _context.Products.UpdateRange(command.Entities);

                await _context.SaveChanges();
                    return command.Entities.Count;
                }
                return 0;
            }
        }
    }
}
