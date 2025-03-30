using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery:IRequest<GetUserByIdResponse>
    {
        public string UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }
        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByIdAsync(new GetUserByIdRequest() { UserID = request.UserId });
        }
    }
}
