using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using AccountService.Application.Features.VehicleAd.Queries.GetAll;

namespace AccountService.Application.Features.VehicleAd.Commands.Create
{
    public class CreateVehicleAdCommand : IRequest<VehicleAdDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CarrierId { get; set; }
        public string VehicleType { get; set; }
        public float Capacity { get; set; }
        public DateTime AdDate { get; set; }
    }

    public class CreateVehicleAdCommandHandler : IRequestHandler<CreateVehicleAdCommand, VehicleAdDto>
    {
        private readonly IVehicleAdService _vehicleAdService;

        public CreateVehicleAdCommandHandler(IVehicleAdService vehicleAdService)
        {
            _vehicleAdService = vehicleAdService;
        }

        public async Task<VehicleAdDto> Handle(CreateVehicleAdCommand request, CancellationToken cancellationToken)
        {
            var vehicleAd = new Domain.Entities.VehicleAd
            {
                Title = request.Title,
                Desc = request.Description,
                Country = request.Country,
                City = request.City,
                userId = request.CarrierId,
                VehicleType = request.VehicleType,
                Capacity = request.Capacity,
                AdDate = request.AdDate,
                Admin1Id = "0",
                Admin2Id = "0",
                Status = (byte)AdStatus.Pending
            };

            var createdAd = await _vehicleAdService.AddAsync(vehicleAd);

            return new VehicleAdDto
            {
                Id = createdAd.Id,
                Title = createdAd.Title,
                Description = createdAd.Desc,
                Country = createdAd.Country,
                City = createdAd.City,
                CarrierId = createdAd.userId,
                CarrierName = createdAd.Carrier?.UserName,
                VehicleType = createdAd.VehicleType,
                Capacity = createdAd.Capacity,
                CreatedDate = createdAd.CreatedDate,
                AdDate = createdAd.AdDate,
                Admin1Id = createdAd.Admin1Id,
                Admin2Id = createdAd.Admin2Id,
                Status = ((AdStatus)createdAd.Status).ToString()
            };
        }
    }
} 