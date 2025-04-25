using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System;

namespace AccountService.Application
{
    public class CreateFeedbackCommand : IRequest<int>
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }

    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, int>
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public CreateFeedbackCommandHandler(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<int> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = new Feedback
            {
                BookingId = request.BookingId,
                UserId = request.UserId,
                Rating = request.Rating,
                Comment = request.Comment,
                Date = request.Date
            };

            await _feedbackRepository.AddAsync(feedback);
            return feedback.FeedbackId;
        }
    }
}