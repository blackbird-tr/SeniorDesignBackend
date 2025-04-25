using AccountService.Application.Interfaces;
using MediatR;
using System;

namespace AccountService.Application
{
    public class UpdateFeedbackCommand : IRequest
    {
        public int FeedbackId { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }

    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand>
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public UpdateFeedbackCommandHandler(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<Unit> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackId);
            if (feedback == null) throw new Exception("Feedback not found");

            feedback.BookingId = request.BookingId;
            feedback.UserId = request.UserId;
            feedback.Rating = request.Rating;
            feedback.Comment = request.Comment;
            feedback.Date = request.Date;

            await _feedbackRepository.UpdateAsync(feedback);
            return Unit.Value;
        }
    }
}