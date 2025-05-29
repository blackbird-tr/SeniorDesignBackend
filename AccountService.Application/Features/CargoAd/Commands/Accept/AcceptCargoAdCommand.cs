using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.Application.Features.CargoAd.Commands.Accept
{
    public class AcceptCargoAdCommand : IRequest<CargoAdDto>
    {
        public int CargoId { get; set; }
        public string AdminId { get; set; }
    }

    public class AcceptCargoAdCommandHandler : IRequestHandler<AcceptCargoAdCommand, CargoAdDto>
    {
        private readonly ICargoAdService _cargoAdService;
        private readonly IAdminService _adminService;
        private readonly IEmailService emailService;
        public AcceptCargoAdCommandHandler(
            ICargoAdService cargoAdService,
            IAdminService adminService,IEmailService email)
        {
            _cargoAdService = cargoAdService;
            _adminService = adminService;
            emailService = email;
        }

        public async Task<CargoAdDto> Handle(AcceptCargoAdCommand request, CancellationToken cancellationToken)
        {
            if (!await _adminService.ExistsAsync(request.AdminId))
                throw new Exception("Geçersiz admin ID");

            var cargoAd = await _cargoAdService.GetByIdAsync(request.CargoId);

            if (cargoAd == null)
                throw new Exception("Cargo ad not found");

            if (cargoAd.Admin1Id == "0")
            {
                cargoAd.Admin1Id = request.AdminId;
            }
            else if (cargoAd.Admin2Id == "0")
            {
                if (cargoAd.Admin1Id == request.AdminId)
                {
                    throw new Exception("Admin already accept");
                }
                cargoAd.Admin2Id = request.AdminId;
            }

            // Eğer her iki admin de onayladıysa
            if (cargoAd.Admin1Id != "0" && cargoAd.Admin2Id != "0")
            {
                cargoAd.Status = (byte)AdStatus.Accepted;
                emailService.SendEmailAsync(cargoAd.Customer.Email, "Cargo Ad Accepted",
                    $"Your cargo ad with title '{cargoAd.Title}' has been accepted by both admins.").Wait();
            }

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