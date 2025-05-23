﻿using System.Linq;
using System.Linq.Dynamic.Core;
using Core.Classes;

using Microsoft.EntityFrameworkCore;
namespace Application.Extentions
{
    public static class IQuerableExtentions
    {
        //Extension Method For Sort And Convert To Pagination List
        public static IQueryable sort(this IQueryable queryable,string sortColumn,string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                return queryable;

            sortOrder = sortOrder?.ToLower() == "desc" ? "descending" : "ascending";
            string ordering = $"{sortColumn} {sortOrder}";

            return queryable.OrderBy(ordering); ;
        }

        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable,int page=1,int pagesize=10)
        {
            return new PaginatedList<T>
            {
                TotalCount = queryable.Count(),
                PageSize = pagesize,
                PageIndex = page,
                Items = await queryable.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync(),
            };
        }
    }
}
