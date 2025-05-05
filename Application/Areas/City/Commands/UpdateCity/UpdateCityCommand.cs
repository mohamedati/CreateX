using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Extentions;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Resources;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Application.Areas.City.Commands.UpdateCity
{
    public class UpdateCityCommand:IRequest
    {
        public int ID {  get; set; }

        public string Name { get; set; } = null!;



    }
    public class UpdateCityCommandHandler(
        IAppDbContext appDbContext,
        IStringLocalizer<Resource> _Localizer,
        IMapper mapper)
        : IRequestHandler<UpdateCityCommand>
    {


        public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var entity =await  appDbContext.Cities
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == request.ID);

            if (entity is null)
                throw new ItemNotFoundException(_Localizer["NotFound"]);
           
            var data = request.Adapt<Createx.Core.Entities.City>(mapper);

             appDbContext.Cities.Update(data);

            await appDbContext.SaveChangesAsync(cancellationToken);




        }
    }
}
