using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoAd.Commands.Reject
{
    public class RejectCargoAdCommand : IRequest<CargoAdDto>
    {
        public int CargoId { get; set; }
        public string AdminId { get; set; }
    }

    public class RejectCargoAdCommandHandler : IRequestHandler<RejectCargoAdCommand, CargoAdDto>
    {
        private readonly ICargoAdService _cargoAdService;

        public RejectCargoAdCommandHandler(ICargoAdService cargoAdService)
        {
            _cargoAdService = cargoAdService;
        }

        public async Task<CargoAdDto> Handle(RejectCargoAdCommand request, CancellationToken cancellationToken)
        {
            var cargoAd = await _cargoAdService.GetByIdAsync(request.CargoId);

            if (cargoAd == null)
                throw new Exception("Cargo ad not found");

            // Admin ID'sini -1 olarak set et
            if (cargoAd.Admin1Id == "0")
            {
                cargoAd.Admin1Id = "-1";
            }
            else if (cargoAd.Admin2Id == "0")
            {
                cargoAd.Admin2Id = "-1";
            }

            // Status'u Rejected olarak set et
            cargoAd.Status = (byte)AdStatus.Rejected;

            await _cargoAdService.UpdateAsync(cargoAd);

            // Güncellenmiş veriyi tekrar çek
            var updatedAd = await _cargoAdService.GetByIdAsync(request.CargoId);

            return new CargoAdDto
            {
                Id = updatedAd.Id,
                UserId = updatedAd.UserId,
                CustomerName = updatedAd.Customer?.UserName,
                Title = updatedAd.Title,
                Description = updatedAd.Description,
                Weight = updatedAd.Weight,
                CargoType = updatedAd.CargoType,
                DropCity = updatedAd.DropCity,
                DropCountry = updatedAd.DropCountry,
                PickCity = updatedAd.PickCity,
                PickCountry = updatedAd.PickCountry,
                currency = updatedAd.currency,
                Price = updatedAd.Price,
                IsExpired = updatedAd.IsExpired,
                CreatedDate = updatedAd.CreatedDate,
                AdDate = updatedAd.AdDate,
                Admin1Id = updatedAd.Admin1Id,
                Admin2Id = updatedAd.Admin2Id,
                Status = ((AdStatus)updatedAd.Status).ToString()
            };
        }
    }
} 