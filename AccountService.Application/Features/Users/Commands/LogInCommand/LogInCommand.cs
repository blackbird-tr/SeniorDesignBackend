using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.LogInCommand
{
    public class LogInCommand:IRequest<LoginUserResponse>
    {
        public LoginUserRequest Request { get; set; }
    }
    public class LogInCommandHandler : IRequestHandler<LogInCommand, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        public LogInCommandHandler(IUserRepository userRepository)
        {   
            _userRepository = userRepository;
        }
        public async Task<LoginUserResponse> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.AuthenticateAsync(request.Request);
        }
    }
}
