using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Extentions;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Product.Commands.DeleteProduct
{
    public  record DeleteProductCommand(int ID):IRequest;


    public class DeleteProductCommandHandler(
      IAppDbContext appDbContext,
      IStringLocalizer<Resource> _Localizer,
      IMapper mapper,
      ICacheService cache)
      : IRequestHandler<DeleteProductCommand>
    {


        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await appDbContext
                .Products
                .FirstOrDefaultAsync(m => m.Id == request.ID);

            if (entity is null)

                throw new ItemNotFoundException(_Localizer["NotFound"]);

       
            appDbContext.Products.Remove(entity);

            await appDbContext.SaveChangesAsync(cancellationToken);


            await cache.ClearAllAsync("*Product*");




        }
    }
}
