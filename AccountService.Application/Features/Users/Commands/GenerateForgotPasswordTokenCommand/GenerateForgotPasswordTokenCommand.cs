using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.GenerateForgotPasswordTokenCommand
{
    public class GenerateForgotPasswordTokenCommand:IRequest<GenerateForgotPasswordTokenResponse>
    {
        public GenerateForgotPasswordTokenRequest Request { get; set; }
    }

    public class GenerateForgotPasswordTokenCommandHandler : IRequestHandler<GenerateForgotPasswordTokenCommand, GenerateForgotPasswordTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        public GenerateForgotPasswordTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public async Task<GenerateForgotPasswordTokenResponse> Handle(GenerateForgotPasswordTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.GenerateForgotPasswordToken(request.Request);
        }
    }
}

