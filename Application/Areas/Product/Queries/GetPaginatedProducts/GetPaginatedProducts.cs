using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.City.Queries;
using Application.Common.Interfaces;
using Application.Extentions;
using Application.Resources;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Product.Queries.GetPaginatedProducts
{
    public  class GetPaginatedProducts:IRequest<PaginatedList<Createx.Core.Entities.Product>>
    {
        public int PageIndex { get; set; } = 1;

        public int Size { get; set; } = 10;

        public string? Search { get; set; } = "";

        public string? SortColumn { get; set; }

        public string? SortOrder { get; set; }

    }

    public class GetPaginatedProductsHandler
        (IAppDbContext context,
        IStringLocalizer<Resource> localizer,
        IMapper mapper)
        : IRequestHandler<GetPaginatedProducts, PaginatedList<Createx.Core.Entities.Product>>
    {
        public async Task<PaginatedList<Createx.Core.Entities.Product>> Handle(GetPaginatedProducts request, CancellationToken cancellationToken)
        {
            return await context.Products
                .Where(a => request.Search.Trim() == "" 
                || a.NameAr.Contains(request.Search)
                || a.NameEn.Contains(request.Search))
                .AsNoTracking()
                  .sort(request.SortColumn, request.SortOrder)
                  .ProjectTo<Createx.Core.Entities.Product>(mapper.ConfigurationProvider)
                  .ToPaginatedListAsync<Createx.Core.Entities.Product>(request.PageIndex, request.Size);
        }
    }
}