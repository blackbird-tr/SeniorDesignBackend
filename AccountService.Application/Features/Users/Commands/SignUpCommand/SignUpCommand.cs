using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.SignUpCommand
{
    public class SignUpCommand:IRequest<RegisterResponse>
    {
        public RegisterRequest request { get; set; }
    }

    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        public SignUpCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }
        public async Task<RegisterResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            return (await _userRepository.RegisterAsync(request.request));
        }
    }
}
