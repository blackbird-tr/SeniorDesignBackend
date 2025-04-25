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
    public class NotificationRepository :GenericRepository<Notification>, INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context):base(context)
        {
        _context = context;
    }

        public Task<IReadOnlyList<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement INotificationRepository methods
    }
}