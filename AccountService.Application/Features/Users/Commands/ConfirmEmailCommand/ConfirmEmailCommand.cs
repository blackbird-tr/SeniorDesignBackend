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
    public class ConfirmEmailCommand:IRequest<ConfirmEmailResponse>
    {
        public string email { get; set; }
        public string code { get; set; }
    }
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailResponse>
    {
        private readonly IUserRepository _userRepository;
        public ConfirmEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        public async Task<ConfirmEmailResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.ConfirmEmailAsync(request.email,request.code);
        }
    }
}
