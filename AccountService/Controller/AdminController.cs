using AccountService.Application.DTOs;
using AccountService.Application.Features.CargoAd.Commands.Accept;
using AccountService.Application.Features.CargoAd.Commands.Reject;
using AccountService.Application.Features.CargoOffer.Commands.Accept;
using AccountService.Application.Features.CargoOffer.Commands.Reject;
using AccountService.Application.Features.VehicleAd.Commands.Accept;
using AccountService.Application.Features.VehicleAd.Commands.Reject;
using AccountService.Application.Features.VehicleOffer.Commands.Accept;
using AccountService.Application.Features.VehicleOffer.Commands.Reject;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IAdminAuthService _adminAuthService;

        public AdminController(ApplicationDbContext context, IAdminAuthService adminAuthService)
        {
            _context = context;
            _adminAuthService = adminAuthService;
        } 
   
           

            

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("loginnn")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var (adminId, jwtToken, refreshToken) = await _adminAuthService.AuthenticateAsync(request.Username, request.Password);
                return Ok(new { AdminId = adminId, JwtToken = jwtToken, RefreshToken = refreshToken });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] AdminLoginDto loginDto)
        {
            try
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Username == loginDto.Username && a.Password == loginDto.Password);

                if (admin != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Login successful",
                        adminId = admin.Id.ToString()
                    });
                }

                return StatusCode(401, new { success = false, message = "Invalid username or password" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "An error occurred during login" });
            }
        }

        // Accept actions
        [HttpPost("AcceptCargoAd")]
        public async Task<IActionResult> AcceptCargoAd([FromBody] AcceptCargoAdCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("AcceptCargoOffer")]
        public async Task<IActionResult> AcceptCargoOffer([FromBody] AcceptCargoOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("AcceptVehicleAd")]
        public async Task<IActionResult> AcceptVehicleAd([FromBody] AcceptVehicleAdCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("AcceptVehicleOffer")]
        public async Task<IActionResult> AcceptVehicleOffer([FromBody] AcceptVehicleOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // Reject actions
        [HttpPost("RejectCargoAd")]
        public async Task<IActionResult> RejectCargoAd([FromBody] RejectCargoAdCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("RejectCargoOffer")]
        public async Task<IActionResult> RejectCargoOffer([FromBody] RejectCargoOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("RejectVehicleAd")]
        public async Task<IActionResult> RejectVehicleAd([FromBody] RejectVehicleAdCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("RejectVehicleOffer")]
        public async Task<IActionResult> RejectVehicleOffer([FromBody] RejectVehicleOfferCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
