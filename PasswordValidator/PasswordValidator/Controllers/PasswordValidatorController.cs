using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordValidator.Business.DTOs;
using PasswordValidator.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordValidator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordValidatorController : ControllerBase
    {
        public IPasswordValidatorService _passwordValidatorService { get; set; }


        private readonly ILogger<PasswordValidatorController> _logger;

        public PasswordValidatorController(IPasswordValidatorService passwordValidatorService, ILogger<PasswordValidatorController> logger)
        {
            _passwordValidatorService = passwordValidatorService;
            _logger = logger;
        }

        /// <summary>
        /// Validate the password to match the following rules:
        /// - Nine or more characters
        /// - At least one digit
        /// - At least one lowercase letter
        /// - At least one uppercase letter
        /// - At least one special character (Consider !@#$%^&amp;&#42;()-+ as special characters)
        /// - No repeated characters
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>
        /// Ok or error list
        /// </returns>
        [Produces(typeof(PasswordValidationResult))]
        [HttpGet]
        public ActionResult Get(string password)
        {
            var result = _passwordValidatorService.Validate(password);
            if(result.Success)
            {
                return Ok(new { success = true });
            }
            return Ok(result);
        }
    }
}
