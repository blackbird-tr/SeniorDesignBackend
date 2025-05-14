using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Feedback.Commands.UpdateFeedback
{
    public class UpdateFeedbackCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public float? Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, bool>
    {
        private readonly IFeedbackService _feedbackService;

        public UpdateFeedbackCommandHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<bool> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackService.GetByIdAsync(request.Id);
            if (feedback == null || !feedback.Active)
                return false;

            feedback.Rating = request.Rating;
            feedback.Comment = request.Comment;
            await _feedbackService.UpdateAsync(feedback);
            return true;
        }
    }
}
