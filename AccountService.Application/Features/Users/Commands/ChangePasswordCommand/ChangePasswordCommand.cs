using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.ChangePasswordCommand
{
    public class ChangePasswordCommand:IRequest<ChangePasswordResponse>
    {
        public string UserId { get; set; }
        public ChangePasswordRequest request { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
       private readonly IUserRepository _userRepository;
        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.ChangePassword(request.request,request.UserId);
        }
    }
}
