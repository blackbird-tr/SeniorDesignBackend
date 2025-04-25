using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Repositories
{
    public class BookingRepository :GenericRepository<Booking>, IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context):base(context)
        {
        _context = context;
    }

        public Task<IReadOnlyList<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking?> GetDetailedBookingAsync(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}