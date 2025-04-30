using AutoMapper;
using MediatR;

namespace API.Extentions
{
    public static class RequestExtensions
    {
        public static T Adapt<T>(this IRequest request,IMapper mapper)
        {
            
            return mapper.Map<T>(request);
        }
    }
}
