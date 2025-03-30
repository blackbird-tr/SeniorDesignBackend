using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.ConfirmEmailCommand
{
    public class ResendConfirmEmailCodeCommand:IRequest<ResendEmailConfirmCodeResponse>
    {
        public string email { get; set; }
    }
    public class ResendConfirmEmailCodeCommandHandler : IRequestHandler<ResendConfirmEmailCodeCommand, ResendEmailConfirmCodeResponse>
    {
        private readonly IUserRepository _userRepository;
        public ResendConfirmEmailCodeCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<ResendEmailConfirmCodeResponse> Handle(ResendConfirmEmailCodeCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.ResendConfirmEmailCodeAsync(request.email);
        }
    }
}
