using MediatR;
using AccountService.Application.Interfaces;
using AccountService.Application.Features.Feedback.Commands.CreateFeedback;

namespace AccountService.Application.Features.Feedback.Queries.GetByUserId
{
    public class GetFeedbacksByUserIdQuery : IRequest<List<FeedbackDto>>
    {
        public string UserId { get; set; }
    }

    public class GetFeedbacksByUserIdQueryHandler : IRequestHandler<GetFeedbacksByUserIdQuery, List<FeedbackDto>>
    {
        private readonly IFeedbackService _feedbackService;

        public GetFeedbacksByUserIdQueryHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<List<FeedbackDto>> Handle(GetFeedbacksByUserIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _feedbackService.GetByUserIdAsync(request.UserId);

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
