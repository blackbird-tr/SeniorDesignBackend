using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IVehicleOfferService : IGenericRepository<VehicleOffer>
    {
        Task<List<VehicleOffer>> GetBySenderIdAsync(string senderId);
        Task<List<VehicleOffer>> GetByReceiverIdAsync(string receiverId);
        Task<List<VehicleOffer>> GetByVehicleAdIdAsync(int vehicleAdId);
        Task<List<VehicleOffer>> GetPendingOffersAsync(string userId);
        Task<bool> UpdateOfferStatusAsync(int offerId, OfferStatus status);
    }
} 