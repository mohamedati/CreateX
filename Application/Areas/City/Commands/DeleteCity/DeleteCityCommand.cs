using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Areas.City.Commands.UpdateCity;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Resources;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Areas.City.Commands.DeleteCity
{
    public record DeleteCityCommand(int ID):IRequest;
    public class DeleteCityCommandHandler(
           IAppDbContext appDbContext,
           IStringLocalizer<Resource> _Localizer,
           IMapper mapper)
           : IRequestHandler<DeleteCityCommand>
    {


        public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var entity = await appDbContext.Cities.FirstOrDefaultAsync(m => m.Id == request.ID);

            if (entity is null)
                throw new ItemNotFoundException(_Localizer["NotFound"]);

            

            appDbContext.Cities.Remove(entity);

            await appDbContext.SaveChangesAsync(cancellationToken);




        }
    }

}
