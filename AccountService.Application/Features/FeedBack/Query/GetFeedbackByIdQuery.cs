using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Feedback.Queries.GetById
{
    public class GetFeedbackByIdQuery : IRequest<FeedbackDto>
    {
        public int Id { get; set; }
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

    public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, FeedbackDto>
    {
        private readonly IFeedbackService _feedbackService;

        public GetFeedbackByIdQueryHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<FeedbackDto?> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _feedbackService.GetByIdAsync(request.Id);
            if (f == null || !f.Active)
                return null;

            return new FeedbackDto
            {
                Id = f.Id,
                BookingId = f.BookingId,
                UserId = f.UserId,
                Rating = f.Rating,
                Comment = f.Comment,
                Date = f.Date
            };
        }
    }
}
