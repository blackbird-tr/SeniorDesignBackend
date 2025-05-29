using AccountService.Application.DTOs;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controller
{
    public class AdminController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("loginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] AdminLoginDto loginDto)
        {
            try
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Username == loginDto.Username && a.Password == loginDto.Password);

                if (admin != null)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Login successful",
                        adminId = admin.Id.ToString()
                    });
                }

                return StatusCode(500, new { success = false, message = "Invalid username or password" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "An error occurred during login" });
            }
        }
    }
} 