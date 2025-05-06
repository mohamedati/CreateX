
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

using ActionExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
using ContentResult = Microsoft.AspNetCore.Mvc.ContentResult;
using Application.Services;




namespace Core.Attributes
{
    public  class CachedAttribute:Attribute ,IAsyncActionFilter
    {
        private readonly int tTL;

        public CachedAttribute(int TTL)
        {
            tTL = TTL;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cachedKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            if (!string.IsNullOrEmpty(await cacheService.GetFromCache(cachedKey))){

                var contentResult = new ContentResult
                {
                    Content = await cacheService.GetFromCache(cachedKey),
                    ContentType = "application/json",
                    StatusCode=200
                    
                   
                };
                context.Result = contentResult;
                return;
            }

            var endPoint = await next.Invoke();
            if(endPoint.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetInCache(cachedKey, okObjectResult.Value,TimeSpan.FromMinutes(tTL));
            }
        }


        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
