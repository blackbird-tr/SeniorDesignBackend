using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Feedback.Queries.GetByBookingId
{
    public class GetFeedbacksByBookingIdQuery : IRequest<List<FeedbackDto>>
    {
        public int BookingId { get; set; }
    }

    public class FeedbackDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public float? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
    }

    public class GetFeedbacksByBookingIdQueryHandler : IRequestHandler<GetFeedbacksByBookingIdQuery, List<FeedbackDto>>
    {
        private readonly IFeedbackService _feedbackService;

        public GetFeedbacksByBookingIdQueryHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<List<FeedbackDto>> Handle(GetFeedbacksByBookingIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _feedbackService.GetByBookingIdAsync(request.BookingId);

            return list.Select(f => new FeedbackDto
            {
                Id = f.Id,
                UserId = f.UserId,
                Rating = f.Rating,
                Comment = f.Comment,
                Date = f.Date
            }).ToList();
        }
    }
}
