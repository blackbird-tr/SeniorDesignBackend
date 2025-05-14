using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Feedback.Queries.GetAll
{
    public class GetAllFeedbacksQuery : IRequest<List<FeedbackDto>> { }

    public class FeedbackDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public float? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
    }

    public class GetAllFeedbacksQueryHandler : IRequestHandler<GetAllFeedbacksQuery, List<FeedbackDto>>
    {
        private readonly IFeedbackService _feedbackService;

        public GetAllFeedbacksQueryHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<List<FeedbackDto>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var list = await _feedbackService.GetAllAsync();
            return list
                .Where(f => f.Active)
                .Select(f => new FeedbackDto
                {
                    Id = f.Id,
                    BookingId = f.BookingId,
                    UserId = f.UserId,
                    Rating = f.Rating,
                    Comment = f.Comment,
                    Date = f.Date
                }).ToList();
        }
    }
}
