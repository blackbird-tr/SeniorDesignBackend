using AccountService.Application.Interfaces;
using MediatR;

namespace AccountService.Application
{
    public class DeleteFeedbackCommand : IRequest
    {
        public int FeedbackId { get; set; }
    }

    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand>
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public DeleteFeedbackCommandHandler(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<Unit> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackId);
            if (feedback == null) throw new Exception("Feedback not found");

            await _feedbackRepository.DeleteAsync(feedback);
            return Unit.Value;
        }
    }
}