using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordValidator.Api.Model;
using PasswordValidator.Business.DTOs;
using PasswordValidator.Business.Enums;
using PasswordValidator.Business.Extensions;
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
        /// Validate the password to match the rules
        /// - Nine or more characters
        /// - At least one digit
        /// - At least one lowercase letter
        /// - At least one uppercase letter
        /// - At least one special character (Consider !@#$%^&amp;&#42;()-+ as special characters)
        /// - No repeated characters
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>
        /// Success or validation errors list
        /// </returns>
        [Produces(typeof(PasswordValidationResult))]
        [HttpPost]
        public ActionResult Validate([FromBody]string password)
        {
            var result = _passwordValidatorService.Validate(password);
            return Ok(new PasswordValidationResultModel
            {
                Success = result.Success,
                Errors = result.Errors?.ToDictionary(t=> (int)t,t=> t.GetDescription())
            });
        }
    }
}
