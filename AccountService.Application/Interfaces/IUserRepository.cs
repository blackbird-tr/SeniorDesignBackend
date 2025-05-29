using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Features.Users.Commands.GenerateForgotPasswordTokenCommand;
using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginUserResponse> AuthenticateAsync(LoginUserRequest request);

        Task<ConfirmEmailResponse> ConfirmEmailAsync(string email, string code); 
        Task<ResendEmailConfirmCodeResponse> ResendConfirmEmailCodeAsync(string email);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest model);
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest model, string userId);
        Task<ValidateRefreshTokenResponse> ValidateRefreshToken(string userId, string token);
        Task<GenerateForgotPasswordTokenResponse> GenerateForgotPasswordToken(GenerateForgotPasswordTokenRequest req);
        Task<ExchangeRefreshTokenResponse> ExchangeRefreshToken(RequestRefreshToken requestRefreshToken);
        Task<GetUserByIdResponse> GetUserByIdAsync(GetUserByIdRequest getUserByIdRequest);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest);
        Task<DeleteUserResponse> DeleteUserAsync(string userId);
    }
}
