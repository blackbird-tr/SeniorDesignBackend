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
    public class PaymentRepository :GenericRepository<Payment>, IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context): base(context)
        {
        _context = context;
    }

        public Task<Payment?> GetLatestPaymentForBookingAsync(int bookingId)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement IPaymentRepository methods
    }
}