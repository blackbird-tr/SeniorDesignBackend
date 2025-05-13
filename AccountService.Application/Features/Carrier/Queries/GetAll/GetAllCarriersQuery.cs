using MediatR;
using AccountService.Application.Interfaces; 
using Microsoft.AspNetCore.Identity;
using AccountService.Domain.Entities;
using AccountService.Application.Features.Carrier.Queries.GetAvailable;

namespace AccountService.Application.Features.Carrier.Queries.GetAll
{
    public class GetAllCarriersQuery : IRequest<List<CarrierDto>> { }

    public class GetAllCarriersQueryHandler : IRequestHandler<GetAllCarriersQuery, List<CarrierDto>>
    {
        private readonly ICarrierService _carrierService;
        private readonly UserManager<User> _userManager;

        public GetAllCarriersQueryHandler(ICarrierService carrierService, UserManager<User> userManager)
        {
            _carrierService = carrierService;
            _userManager = userManager;
        }

        public async Task<List<CarrierDto>> Handle(GetAllCarriersQuery request, CancellationToken cancellationToken)
        {
            var carriers = await _carrierService.GetAllAsync();

            var dtoList = new List<CarrierDto>();

            foreach (var carrier in carriers)
            {
                var user = await _userManager.FindByIdAsync(carrier.UserId); // elle çekiyoruz

                dtoList.Add(new CarrierDto
                {
                    Id = carrier.Id,
                    LicenseNumber = carrier.LicenseNumber,
                    AvailabilityStatus = carrier.AvailabilityStatus,
                    UserId = carrier.UserId,
                    UserName = user?.UserName,
                    Email = user?.Email
                });
            }

            return dtoList;
        }
    }
}
