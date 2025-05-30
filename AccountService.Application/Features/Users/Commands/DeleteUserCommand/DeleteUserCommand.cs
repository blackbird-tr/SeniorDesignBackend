//using AccountService.Application.Common.DTOs.User;
//using AccountService.Application.Interfaces;
//using MediatR;
//using System.Threading;
//using System.Threading.Tasks;

//namespace AccountService.Application.Features.Users.Commands.DeleteUserCommand
//{
//    public class DeleteUserCommand : IRequest<DeleteUserResponse>
//    {
//        public string UserId { get; set; }
//    }

//    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
//    {
//        private readonly IUserRepository _userRepository;

//        public DeleteUserCommandHandler(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
//        {
//            return await _userRepository.DeleteUserAsync(request.UserId);
//        }
//    }
//} 