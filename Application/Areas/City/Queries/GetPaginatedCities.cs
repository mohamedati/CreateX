using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Extentions;
using Application.Common.Interfaces;
using Application.Resources;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Classes;
using Createx.Core.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;

namespace Application.Areas.City.Queries
{
    public  class GetPaginatedCities:IRequest<PaginatedList<Createx.Core.Entities.City>>
    {
        public int PageIndex { get; set; }

        public int Size {  get; set; }

        public string? Search { get; set; } = "";

        public string? SortColumn { get; set; }

        public string? SortOrder { get; set; } 

    }

    public class GetPaginatedCitiesHandler
        (IAppDbContext context,
        IStringLocalizer<Resource> localizer,
        IMapper mapper)
        : IRequestHandler<GetPaginatedCities, PaginatedList<Createx.Core.Entities.City>>
    {
        public async Task<PaginatedList<Createx.Core.Entities.City>> Handle(GetPaginatedCities request, CancellationToken cancellationToken)
        {
            return await context.Cities
                .Where(a => request.Search.Trim() == "" || a.Name.Contains(request.Search))
                .AsNoTracking()
                  .sort(request.SortColumn, request.SortOrder)
                  .ProjectTo<Createx.Core.Entities.City>(mapper.ConfigurationProvider)
                  .ToPaginatedListAsync<Createx.Core.Entities.City>(request.PageIndex, request.Size);
        }
    }
}
