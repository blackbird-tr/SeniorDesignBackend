using MediatR;
using AccountService.Application.Interfaces;

namespace AccountService.Application.Features.Feedback.Commands.DeleteFeedback
{
    public class DeleteFeedbackCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, bool>
    {
        private readonly IFeedbackService _feedbackService;

        public DeleteFeedbackCommandHandler(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<bool> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackService.GetByIdAsync(request.Id);
            if (feedback == null)
                return false;

            feedback.Active = false;
            await _feedbackService.UpdateAsync(feedback);
            return true;
        }
    }
}
