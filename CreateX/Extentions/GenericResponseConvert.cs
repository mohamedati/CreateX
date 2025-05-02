using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Extentions
{
    public static class GenericResponseConvert
    {

        public static async Task<ActionResult> ToGenericResponse<T>(this Task<T> Target)
        {

            var result=await Target;

            var res= new GenericResponse
            {
                IsSuccess = true,
                Data = result



            };

            return new OkObjectResult(res);




        }


        public static async Task<ActionResult> ToGenericResponse(this Task Target)
        {

            await Target;



            var res = new GenericResponse
            {
                IsSuccess = true,
                Data = null



            };

            return new OkObjectResult(res);




        }
    }
}
