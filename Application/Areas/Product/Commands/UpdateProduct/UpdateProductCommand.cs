using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Extentions;
using Application.Areas.City.Commands.CreateCity;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Product.Commands
{
    public  class UpdateProductCommand:IRequest
    {
        public int ID {  get; set; } 
        public string NameAr { get; set; } = null!;


 
        public string? NameEn { get; set; }

        public int? ParentProductId { get; set; }

        public int StoreId { get; set; }


        public double SalePrice { get; set; }

        public double PurchasePrice { get; set; }

        public string ProductId { get; set; } = null!;

        public string Barcode { get; set; } = null!;

        public string? ExtraBarcode { get; set; }

        public int TypeId { get; set; }



        public IFormFile Photo { get; set; } = null!;
    }

    public class UpdateProductCommandHandler(
      IAppDbContext appDbContext,
      IStringLocalizer<Resource> _Localizer,
      IMapper mapper,
      ICacheService cache)
      : IRequestHandler<UpdateProductCommand>
    {


        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity=await appDbContext
                .Products
                .FirstOrDefaultAsync(m=>m.Id==request.ID);

            if (entity is null)

                throw new ItemNotFoundException(_Localizer["NotFound"]);
           
            byte[] imageData = null;


            using (var ms = new MemoryStream())
            {
                await request.Photo.CopyToAsync(ms);
                imageData = ms.ToArray();
            }

            var data = request.Adapt<Createx.Core.Entities.Product>(mapper);
            data.Id = request.ID;
           
            data.Photo = imageData;
             appDbContext.Products.Update(data);

            await appDbContext.SaveChangesAsync(cancellationToken);


            await cache.ClearAllAsync("*Product*");




        }
    }
}
