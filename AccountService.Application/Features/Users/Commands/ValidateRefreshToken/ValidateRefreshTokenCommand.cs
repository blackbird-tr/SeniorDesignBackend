using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.ValidateRefreshToken
{
    public class ValidateRefreshTokenCommand:IRequest<ValidateRefreshTokenResponse>
    {
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
    }
    public class ValidateRefreshTokenHandler : IRequestHandler<ValidateRefreshTokenCommand, ValidateRefreshTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        public ValidateRefreshTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public async Task<ValidateRefreshTokenResponse> Handle(ValidateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.ValidateRefreshToken(request.UserId, request.RefreshToken);
        }
    }
}
