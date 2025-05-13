using MediatR;
using Microsoft.AspNetCore.Identity;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Carrier.Queries.GetAvailable
{
    public class GetAvailableCarriersQuery : IRequest<List<CarrierDto>> { }

    public class CarrierDto
    {
        public int Id { get; set; }
        public string LicenseNumber { get; set; }
        public bool AvailabilityStatus { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class GetAvailableCarriersQueryHandler : IRequestHandler<GetAvailableCarriersQuery, List<CarrierDto>>
    {
        private readonly ICarrierService _carrierService;
        private readonly UserManager<User> _userManager;

        public GetAvailableCarriersQueryHandler(ICarrierService carrierService, UserManager<User> userManager)
        {
            _carrierService = carrierService;
            _userManager = userManager;
        }

        public async Task<List<CarrierDto>> Handle(GetAvailableCarriersQuery request, CancellationToken cancellationToken)
        {
            var carriers = await _carrierService.GetAvailableCarriersAsync();
            var dtoList = new List<CarrierDto>();

            foreach (var carrier in carriers)
            {
                var user = await _userManager.FindByIdAsync(carrier.UserId);

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
