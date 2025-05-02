
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Serilog;
using Infrastructure.DbContext;
using System.Text;
using Application.Resources;
using Application.Common.Models;
using Application.Common.Exceptions;

namespace CreateX.API.MiddleWares
{

    public class GlobalErrorMiddleware(RequestDelegate next, IStringLocalizer<Resource> localizer)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Enable buffering at the start to capture the request body
            context.Request.EnableBuffering();

            try
            {
                await next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error(ex.Message, "UnAuthorized");
                await _buildResponse(context, ex.Message, HttpStatusCode.Unauthorized);

            }
            catch(ValidationException ex)
            {

                Log.Error(ex.Message, "ValidatonErrors");
                await _buildResponse(context, ex, HttpStatusCode.BadRequest);
            }
            catch (ItemNotFoundException ex)
            {
                Log.Error(ex.Message, "Item Not Found");
                await _buildResponse(context, ex.Message, HttpStatusCode.NotFound);
            }catch(Exception ex)
            {

                Log.Error(ex.Message, "Exception");
                await _buildResponse(context, ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        private static async Task _buildResponse(HttpContext context, string msg, HttpStatusCode httpStatus)
        {
            context.Response.StatusCode = (int)httpStatus;

            await context.Response.WriteAsJsonAsync(new GenericResponse { IsSuccess = false, Message = msg });
        }


        private static async Task _buildResponse(HttpContext context, ValidationException ex, HttpStatusCode httpStatus)
        {
            context.Response.StatusCode = (int)httpStatus;

            var genericRes = new GenericResponse
            {
                IsSuccess = false,
                Message=ex.Message,
                Errors=ex.Errors
            };


            await context.Response.WriteAsJsonAsync(genericRes);

        }


    }

}
