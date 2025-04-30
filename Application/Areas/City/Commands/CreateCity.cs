
using Application.Resources;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using Createx.Core.Entities;
using API.Extentions;
using AutoMapper;

namespace Application.Areas.City.Commands
{
    public  class CreateCity:IRequest
    {
        public string Name { get; set; } = null!;
    }


    public class CreateCityHandler (
        IAppDbContext appDbContext,
        IStringLocalizer<Resource> _Localizer,
        IMapper mapper)
        : IRequestHandler<CreateCity>
    {
     

        public async Task Handle(CreateCity request, CancellationToken cancellationToken)
        {
            var data=request.Adapt<Createx.Core.Entities.City>(mapper);

           await  appDbContext.Cities.AddAsync(data);

            await appDbContext.SaveChangesAsync(cancellationToken);




        }
    }
}
