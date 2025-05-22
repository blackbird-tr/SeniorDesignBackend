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
    public class CargoOfferRepository : GenericRepository<CargoOffer>, ICargoOfferService
    {
        private readonly ApplicationDbContext _context;

        public CargoOfferRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<CargoOffer> GetByIdAsync(int id)
        {
            return await _context.CargoOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.CargoAd)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public override async Task<IReadOnlyList<CargoOffer>> GetAllAsync()
        {
            return await _context.CargoOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.CargoAd)
                .ToListAsync();
        }

        public async Task<List<CargoOffer>> GetBySenderIdAsync(string senderId)
        {
            return await _context.CargoOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.CargoAd)
                .Where(o => o.SenderId == senderId && o.Active)
                .ToListAsync();
        }

        public async Task<List<CargoOffer>> GetByReceiverIdAsync(string receiverId)
        {
            return await _context.CargoOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.CargoAd)
                .Where(o => o.ReceiverId == receiverId && o.Active)
                .ToListAsync();
        }

        public async Task<List<CargoOffer>> GetByCargoAdIdAsync(int cargoAdId)
        {
            return await _context.CargoOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.CargoAd)
                .Where(o => o.CargoAdId == cargoAdId && o.Active)
                .ToListAsync();
        }

        public async Task<List<CargoOffer>> GetPendingOffersAsync(string userId)
        {
            return await _context.CargoOffers
                .Include(o => o.Sender)
                .Include(o => o.Receiver)
                .Include(o => o.CargoAd)
                .Where(o => (o.SenderId == userId || o.ReceiverId == userId) 
                    && o.Status == OfferStatus.Pending 
                    && o.Active)
                .ToListAsync();
        }

        public async Task<bool> UpdateOfferStatusAsync(int offerId, OfferStatus status)
        {
            var offer = await GetByIdAsync(offerId);
            if (offer == null) return false;

            offer.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 