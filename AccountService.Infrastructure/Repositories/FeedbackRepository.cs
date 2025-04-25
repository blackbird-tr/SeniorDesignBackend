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
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
    private readonly ApplicationDbContext _context;

    public FeedbackRepository(ApplicationDbContext context):base(context)
        {
        _context = context;
    }

        public Task<IReadOnlyList<Feedback>> GetFeedbacksByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        // TODO: Implement IFeedbackRepository methods
    }
}