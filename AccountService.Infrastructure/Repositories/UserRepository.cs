using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Common.Helpers;
using AccountService.Application.Features.Users.Commands.GenerateForgotPasswordTokenCommand;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _jWTSettings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly ApplicationDbContext _context;
        public UserRepository(UserManager<User> userManager, IOptions<JWTSettings> jwtSettings, SignInManager<User> signInManager, JwtSecurityTokenHandler jwtSecurityTokenHandler, ApplicationDbContext application_context):base(application_context)
        {
            _jWTSettings = jwtSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _context = application_context;
        }
         

        public async Task<LoginUserResponse> AuthenticateAsync(LoginUserRequest request)
        {
           
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null) { throw new Exception("User Not Found with this email"); }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new Exception("Invalid Credential");
            var emailconfirmed=await _userManager.IsEmailConfirmedAsync(user);
            if (emailconfirmed == false) throw new Exception("Email not confirmed");

            JwtSecurityToken jwtSecurityToken = await GenerateJwTokenAsync(user);
            RefreshToken retok = GenerateRefreshToken();
            LoginUserResponse response=new LoginUserResponse() { UserId=user.Id,emailValid=emailconfirmed,JwToken=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),RefreshToken=retok.Token };
            _context.Set<RefreshToken>().Add(new RefreshToken()
            {
                UserID = user.Id,
                Token = retok.Token,
                Expires = retok.Expires,
                Created = retok.Created,
                CreatedByIp = retok.CreatedByIp,
                Revoked = retok.Revoked,
                RevokedByIp = retok.RevokedByIp,
                ReplacedByToken = retok.ReplacedByToken
            });
            _context.SaveChanges();
            return response;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest model, string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded) throw new Exception("There occur an error");
            return new ChangePasswordResponse() { UserId = user.Id, Message = "Password changed Sucessfully" };
        }

        public async Task<ConfirmEmailResponse> ConfirmEmailAsync(string email, string code)
        {
         var user=await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");
            var result=await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded) throw new Exception("There occur an error");
            return new ConfirmEmailResponse() { UserId = user.Id, Message = "Email Confirmed." };
        }

        public async Task<ExchangeRefreshTokenResponse> ExchangeRefreshToken(RequestRefreshToken requestRefreshToken)
        {
            ExchangeRefreshTokenResponse response;
            try
            {
                //Getting user claims from token
                ClaimsPrincipal claimsPrincipal = ValidateToken(requestRefreshToken.AccessToken, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key)),
                    ValidateLifetime = false // we check expired tokens here
                });
                // invalid token/signing key was passed and we can't extract user claims
                if (claimsPrincipal == null)
                    throw new Exception("TokenError");
                //Getting user from repository

                //var user = await _userManager.GetUserAsync(claimsPrincipal);
                var myuserId = _context.RefreshTokens.Where(a => a.Token == requestRefreshToken.RefreshToken).Select(f => f.UserID).FirstOrDefault();
                User user = await _userManager.FindByIdAsync(myuserId);
                //generating new access token
                var jwtToken = await GenerateJwTokenAsync(user);
                //generating new refresh token
                var refreshToken = GenerateRefreshToken();
                // delete the refresh token that exchanged
                var deleterefreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == requestRefreshToken.RefreshToken);

                if (deleterefreshToken != null)
                {
                    _context.RefreshTokens.Remove(deleterefreshToken);
                    _context.SaveChanges();
                }
                _context.RefreshTokens.Add(new RefreshToken
                {

                    Token = refreshToken.Token,
                    Expires = refreshToken.Expires,
                    Created = refreshToken.Created,
                    CreatedByIp = refreshToken.CreatedByIp,
                    Revoked = refreshToken.Revoked,
                    RevokedByIp = refreshToken.RevokedByIp,
                    ReplacedByToken = refreshToken.ReplacedByToken,
                    UserID = user.Id,

                });


                _context.SaveChanges();
                await this.UpdateAsync(user);
                response = new ExchangeRefreshTokenResponse { Message = refreshToken.Token };

            }
            catch (Exception ex)
            {
                response = new ExchangeRefreshTokenResponse { Message = ex.Message };
            }
            return response;

        }
        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) throw new Exception("User not found");
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded) throw new Exception("There occur an error");
            return new ForgotPasswordResponse() { Message = "Password changed." };
        }
        
        public async Task<GenerateForgotPasswordTokenResponse> GenerateForgotPasswordToken(GenerateForgotPasswordTokenRequest req)
        {
            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null) throw new Exception("User not found");
            var result = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (result == null) throw new Exception("there occur an error");
            return new GenerateForgotPasswordTokenResponse() { Message = result };
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
           var userwithSameUserName=await _userManager.FindByNameAsync(request.UserName);
            if (userwithSameUserName != null) { throw new Exception("Already Used Username"); }

            var userwithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userwithSameEmail != null) { throw new Exception("Already Used Email"); }

            var user = new User()
            {
                Email = request.Email,
                BirthYear = request.BirthYear,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber 
            };
            var result=await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded) { throw new Exception("There occur an error"); }
            var code =await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new RegisterResponse() { Message = code, UserId = user.Id, UserName = user.UserName };
        }

        public async Task<ResendEmailConfirmCodeResponse> ResendConfirmEmailCodeAsync(string email)
        {
             var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("user not found");
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (isConfirmed) { throw new Exception("email already confirmed"); }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new ResendEmailConfirmCodeResponse { Message = code };
        }

        public async Task<ValidateRefreshTokenResponse> ValidateRefreshToken(string userId, string token)
        {
            RefreshToken rf = await _context.RefreshTokens.Where(r => r.Token == token).FirstOrDefaultAsync();
            if(rf.UserID==userId && rf.Token==token && rf.IsActive)
            {
                return new ValidateRefreshTokenResponse { UserId = userId, Message = "Succeded" };
            }
            else
            {
                return new ValidateRefreshTokenResponse { UserId = userId, Message = "Refresh Token not Invalid" };
            }

            throw new InvalidOperationException();
        }

        public async Task<JwtSecurityToken> GenerateJwTokenAsync(User user)
        {
            var userClaim=await _userManager.GetClaimsAsync(user);

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id),
                new Claim("ip", ipAddress),
                new Claim(ClaimTypes.Name, user.UserName)
            }.Union(userClaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
              issuer: _jWTSettings.Issuer,
              audience: _jWTSettings.Audience,
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinutes),
              signingCredentials: signingCredentials);
            return jwtSecurityToken;

        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = IpHelper.GetIpAddress()
            };
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public async Task<GetUserByIdResponse> GetUserByIdAsync(GetUserByIdRequest getUserByIdRequest)
        {
           var user=await _userManager.FindByIdAsync(getUserByIdRequest.UserID);
            if (user == null) { throw new Exception("User not found"); }
            return new GetUserByIdResponse() { BirthYear = user.BirthYear,Email=user.Email,Name=user.FirstName,PhoneNumber=user.PhoneNumber,Surname=user.LastName };
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var user = await _userManager.FindByIdAsync(updateUserRequest.UserId);
            if (user == null) { throw new Exception("User not found"); }

            // Sadece güncellenmesi gereken alanları değiştir
            user.BirthYear = updateUserRequest.BirthYear;
            user.FirstName = updateUserRequest.FirstName;
            user.LastName = updateUserRequest.LastName;

            _context.Users.Update(user); // Bu satır aslında gereksiz, SaveChanges zaten takip ediyor
            await _context.SaveChangesAsync();

            return new UpdateUserResponse() { UserID = user.Id, Message = "Updated." };

             
        }
    }
}
