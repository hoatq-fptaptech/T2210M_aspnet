﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T2210M_API.Models.Auth;
using T2210M_API.Entities;
using BCrypt.Net;
using T2210M_API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2210M_API.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly T2210mApiContext _context;
        private readonly IConfiguration _config;

        public AuthController(T2210mApiContext context,IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        private string GenJWT(User user)
        {
            string key = _config["JWT:Key"];
            int lifeTime = Convert.ToInt32(_config["JWT:Lifetime"]);
            string issuer = _config["JWT:Issuer"];
            string audience = _config["JWT:Audience"];

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signatureKey = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.FullName)
            };

            var token = new JwtSecurityToken(
                    issuer,
                    audience,
                    payload,
                    expires: DateTime.Now.AddMinutes(lifeTime),
                    signingCredentials: signatureKey
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized("Vui lòng điền đầy đủ thông tin");
            }
            try
            {
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var pwd_hashed = BCrypt.Net.BCrypt.HashPassword(model.password,salt);
                var user = new User
                {
                    Email = model.email,
                    FullName = model.full_name,
                    Password = pwd_hashed

                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(new UserDTO
                {
                    id = user.Id,
                    email = user.Email,
                    full_name = user.FullName,
                    token = GenJWT(user)
                });
            }
            catch (Exception e) {
                return Unauthorized("Vui lòng điền đầy đủ thông tin");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized("Email hoặc mật khẩu không đúng");
            }
            try
            {
                var user = _context.Users.Where(u => u.Email.Equals(model.email)).First();
                if(user == null)
                {
                    throw new Exception("Email hoặc mật khẩu không đúng");
                }
                bool verified = BCrypt.Net.BCrypt.Verify(model.password, user.Password);
                if (!verified)
                {
                    throw new Exception("Email hoặc mật khẩu không đúng");
                }
                return Ok(new UserDTO
                {
                    id = user.Id,
                    email = user.Email,
                    full_name = user.FullName,
                    token = GenJWT(user)
                });
            }
            catch(Exception e)
            {
                return Unauthorized("Email hoặc mật khẩu không đúng");
            }
        }

        [HttpGet]
        [Route("myprofile")]
        public IActionResult MyProfile()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!identity.IsAuthenticated)
            {
                return Unauthorized("Not Autheticated");
            }
            try
            {
                var userClaims = identity.Claims;
                var userId = userClaims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _context.Users.Find(Convert.ToInt32(userId));
                return Ok(new UserDTO
                {
                    email = user.Email,
                    full_name = user.FullName
                });
                // chỗ này thực ra phải có 1 DTO dành riêng cho profile gồm nhiều thông tin hơn
                // ví dụ: gồm cả address, tel, cart, orders ....
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}

