using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Commands.ExchangeRefreshToken
{
    public class ExchangeRefreshTokenCommand:IRequest<ExchangeRefreshTokenResponse>
    {
        public RequestRefreshToken requestRefreshToken { get; set; }

    }
    public class ExchangeRefreshTokenHandler : IRequestHandler<ExchangeRefreshTokenCommand, ExchangeRefreshTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        public ExchangeRefreshTokenHandler(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        public async Task<ExchangeRefreshTokenResponse> Handle(ExchangeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.ExchangeRefreshToken(request.requestRefreshToken);
        }
    }
}
