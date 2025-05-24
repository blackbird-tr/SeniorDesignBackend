using AccountService.Application.Common.DTOs.User;
using AccountService.Application.Features.Users.Commands.ChangePasswordCommand;
using AccountService.Application.Features.Users.Commands.ConfirmEmailCommand;
using AccountService.Application.Features.Users.Commands.ExchangeRefreshToken;
using AccountService.Application.Features.Users.Commands.ForgotPasswordCommand;
using AccountService.Application.Features.Users.Commands.GenerateForgotPasswordTokenCommand;
using AccountService.Application.Features.Users.Commands.LogInCommand;
using AccountService.Application.Features.Users.Commands.SignUpCommand;
using AccountService.Application.Features.Users.Commands.UpdateUserCommand;
using AccountService.Application.Features.Users.Commands.ValidateRefreshToken;
using AccountService.Application.Features.Users.Queries.GetUserByIdQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.WebApi.Controller
{
    public class UserController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> SignUpUser(RegisterRequest registerRequest)
        {
            SignUpCommand signUpCommand = new SignUpCommand();
            signUpCommand.request = registerRequest;

            return Ok(await Mediator.Send(signUpCommand));
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginUserRequest loginRequest)
        {
            LogInCommand logInCommand = new LogInCommand();
            logInCommand.Request = loginRequest;

            return Ok(await Mediator.Send(logInCommand));
        }
        [AllowAnonymous]
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email,string token)
        {
            ConfirmEmailCommand confirmEmailCommand = new ConfirmEmailCommand();
            confirmEmailCommand.code = token;
            confirmEmailCommand.email = email;

            return Ok(await Mediator.Send(confirmEmailCommand));
        }
        [AllowAnonymous]
        [HttpPost("ResendEmailConfirmCode")]
        public async Task<IActionResult> ResendEmailConfirmCode(string email)
        {
            ResendConfirmEmailCodeCommand confirmEmailCommand = new ResendConfirmEmailCodeCommand(); 
            confirmEmailCommand.email = email;

            return Ok(await Mediator.Send(confirmEmailCommand));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(
         [FromHeader(Name = "User-Id")] string userId,
         [FromBody] ChangePasswordRequest changePasswordRequest)
        {
            ChangePasswordCommand changePasswordCommand = new ChangePasswordCommand
            {
                request = changePasswordRequest,
                UserId = userId
            };

            return Ok(await Mediator.Send(changePasswordCommand));
        }
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            ForgotPasswordCommand forgotPasswordCommand = new ForgotPasswordCommand();
            forgotPasswordCommand.request = forgotPasswordRequest;
            

            return Ok(await Mediator.Send(forgotPasswordCommand));
        }
        [HttpPost("GenerateForgotPasswordToken")]
        public async Task<IActionResult> GenerateForgotPasswordToken(GenerateForgotPasswordTokenRequest Request)
        {
            GenerateForgotPasswordTokenCommand generateForgotPasswordTokenCommand = new GenerateForgotPasswordTokenCommand();
            generateForgotPasswordTokenCommand.Request = Request;


            return Ok(await Mediator.Send(generateForgotPasswordTokenCommand));
        }
        [HttpPost("ValidateRefreshToken")]
        public async Task<IActionResult> ValidateRefreshToken(string userId,string RefreshToken)
        {
            ValidateRefreshTokenCommand validateRefreshTokenCommand = new ValidateRefreshTokenCommand();
            validateRefreshTokenCommand.RefreshToken = RefreshToken;
            validateRefreshTokenCommand.UserId = userId;    


            return Ok(await Mediator.Send(validateRefreshTokenCommand));
        }

        [HttpPost("ExchangeRefreshToken")]
        public async Task<IActionResult> ExchangeRefreshToken(RequestRefreshToken requestRefreshToken)
        {
           ExchangeRefreshTokenCommand exchangeRefreshTokenCommand = new ExchangeRefreshTokenCommand();
            exchangeRefreshTokenCommand.requestRefreshToken = requestRefreshToken;


            return Ok(await Mediator.Send(exchangeRefreshTokenCommand));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery();
            getUserByIdQuery.UserId = id;


            return  Ok(await Mediator.Send(getUserByIdQuery));
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser(string id,UpdateUserRequest req)
        {
             UpdateUserCommand updateUserCommand = new UpdateUserCommand();
            updateUserCommand.userRequest=req;


            return Ok(await Mediator.Send(updateUserCommand));
        }
    }
}
