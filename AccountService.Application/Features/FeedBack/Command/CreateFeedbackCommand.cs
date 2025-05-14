using MediatR;
using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using AccountService.Domain.Entities;

namespace AccountService.Application.Features.Feedback.Commands.CreateFeedback
{
    public class CreateFeedbackCommand : IRequest<FeedbackDto>
    {
        public int BookingId { get; set; }
        public string UserId { get; set; } // artık string!
        public float? Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class FeedbackDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public float? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
    }

    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, FeedbackDto>
    {
        private readonly IFeedbackService _feedbackService;
        private readonly UserManager<User> _userManager;

        public CreateFeedbackCommandHandler(
            IFeedbackService feedbackService,
            UserManager<User> userManager)
        {
            _feedbackService = feedbackService;
            _userManager = userManager;
        }

        public async Task<FeedbackDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
              if (user == null)
                {
                    throw new Exception("User not found.");
                }

            var feedback = new Domain.Entities.Feedback
            {
                BookingId = request.BookingId,
                UserId = request.UserId, // artık string!
                Rating = request.Rating,
                Comment = request.Comment,
                Date = DateTime.UtcNow,
                Active = true
            };

            var created = await _feedbackService.AddAsync(feedback);

            return new FeedbackDto
            {
                Id = created.Id,
                BookingId = created.BookingId,
                UserId = created.UserId,
                Rating = created.Rating,
                Comment = created.Comment,
                Date = created.Date
            };
        }
    }
}
