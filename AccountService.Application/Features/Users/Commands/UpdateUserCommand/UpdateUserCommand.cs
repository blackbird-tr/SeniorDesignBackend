using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommand:IRequest<UpdateUserResponse>
    {
        public UpdateUserRequest userRequest { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateUser(request.userRequest);
        }
    }
}
