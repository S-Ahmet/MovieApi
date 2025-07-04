using MoviApi.Aplication.Features.CQRSDesingPattern.Commands.CategoryCommands;
using MoviApi.Domain.Entities;
using MovieApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviApi.Aplication.Features.CQRSDesingPattern.Handlers.CategoryHandlers
{
    public class CreateCategoryCommandHandler
    {

        private readonly MovieContext _context;

        public CreateCategoryCommandHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCategoryCommand command)
        {
            _context.Categories.Add(new Category
            {
                CategoryName = command.CategoryName
            });
            await _context.SaveChangesAsync();

        }
    }
}
