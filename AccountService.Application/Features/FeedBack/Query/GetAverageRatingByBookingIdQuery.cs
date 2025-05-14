using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Feedback.Queries.GetAverageRating
{
    public class GetAverageRatingByBookingIdQuery : IRequest<float?>
    {
        public int BookingId { get; set; }
    }

    public class GetAverageRatingByBookingIdQueryHandler : IRequestHandler<GetAverageRatingByBookingIdQuery, float?>
    {
        private readonly IFeedbackService _feedbackService;

        public GetAverageRatingByBookingIdQueryHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<float?> Handle(GetAverageRatingByBookingIdQuery request, CancellationToken cancellationToken)
        {
            return await _feedbackService.GetAverageRatingByBookingIdAsync(request.BookingId);
        }
    }
}
