using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Repositories
{
    public class VehicleOfferRepository : GenericRepository<VehicleOffer>, IVehicleOfferService
    {
        private readonly ApplicationDbContext _context;

        public VehicleOfferRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<VehicleOffer> GetByIdAsync(int id)
        {
            return await _context.VehicleOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.VehicleAd)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public override async Task<IReadOnlyList<VehicleOffer>> GetAllAsync()
        {
            return await _context.VehicleOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.VehicleAd)
                .ToListAsync();
        }

        public async Task<List<VehicleOffer>> GetBySenderIdAsync(string senderId)
        {
            return await _context.VehicleOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.VehicleAd)
                .Where(o => o.SenderId == senderId && o.Active)
                .ToListAsync();
        }

        public async Task<List<VehicleOffer>> GetByReceiverIdAsync(string receiverId)
        {
            return await _context.VehicleOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.VehicleAd)
                .Where(o => o.ReceiverId == receiverId && o.Active)
                .ToListAsync();
        }

        public async Task<List<VehicleOffer>> GetByVehicleAdIdAsync(int vehicleAdId)
        {
            return await _context.VehicleOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.VehicleAd)
                .Where(o => o.VehicleAdId == vehicleAdId && o.Active)
                .ToListAsync();
        }

        public async Task<List<VehicleOffer>> GetPendingOffersAsync(string userId)
        {
            return await _context.VehicleOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.VehicleAd)
                .Where(o => (o.SenderId == userId || o.ReceiverId == userId) 
                    && o.Status == OfferStatus.Pending.ToString() 
                    && o.Active)
                .ToListAsync();
        }

        public async Task<bool> UpdateOfferStatusAsync(int offerId, string status)
        {
            var offer = await GetByIdAsync(offerId);
            if (offer == null) return false;

            offer.Status =status;

            _context.Update(offer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 