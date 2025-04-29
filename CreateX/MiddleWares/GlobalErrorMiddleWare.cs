using System.ComponentModel.DataAnnotations;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Serilog;
using Infrastructure.DbContext;

namespace CreateX.API.MiddleWares
{
    
        public class GlobalErrorMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Enable buffering at the start to capture the request body
            context.Request.EnableBuffering();

            try
            {
                await next(context);
            }
            catch(UnauthorizedAccessException ex)
            {
                Log.Error(ex.Message, ex);

            }
          
        }

    }

}
