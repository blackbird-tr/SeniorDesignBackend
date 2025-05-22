using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface ICargoOfferService : IGenericRepository<CargoOffer>
    {
        Task<List<CargoOffer>> GetBySenderIdAsync(string senderId);
        Task<List<CargoOffer>> GetByReceiverIdAsync(string receiverId);
        Task<List<CargoOffer>> GetByCargoAdIdAsync(int cargoAdId);
        Task<List<CargoOffer>> GetPendingOffersAsync(string userId);
        Task<bool> UpdateOfferStatusAsync(int offerId, OfferStatus status);
    }
} 