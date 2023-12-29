﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T2210M_API.Models.Auth;
using T2210M_API.Entities;
using BCrypt.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2210M_API.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly T2210mApiContext _context;

        public AuthController(T2210mApiContext context)
        {
            _context = context;
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
                return Ok();
            }
            catch (Exception e) {
                return Unauthorized("Vui lòng điền đầy đủ thông tin");
            }
        }
    }
}
