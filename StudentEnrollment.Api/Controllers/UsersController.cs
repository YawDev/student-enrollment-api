using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.Core;
using StudentEnrollment.Core.Commands;
using StudentEnrollment.Core.Dtos;
using StudentEnrollment.Core.Exceptions;
using StudentEnrollment.Core.Queries;

namespace StudentEnrollment.API.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly Message _messages;
        public UsersController(ILogger<UsersController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }

        [HttpPost("student-enrollment/api/login")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ValidateLogin([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                ValidateLogin command = new ValidateLogin(loginDto);
                try
                {
                    var result = _messages.Dispatch(command);
                    return FromResult(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

        [HttpPost("student-enrollment/api/instructors/register")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult InstructorRegistration([FromBody] AddInstructorDto AddInstructorDto)
        {
            if (ModelState.IsValid)
            {
                AddInstructor command = new AddInstructor(AddInstructorDto);
                try
                {
                    var result = _messages.Dispatch(command);
                    return FromResult(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

        [HttpPost("student-enrollment/api/students/register")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult StudentRegistration([FromBody] AddStudentDto addStudentDto)
        {
            if (ModelState.IsValid)
            {
                AddStudent command = new AddStudent(addStudentDto);
                try
                {
                    var result = _messages.Dispatch(command);
                    return FromResult(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }


        [HttpPost("student-enrollment/api/admin/register")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AdminRegistration([FromBody] AddAdminDto addAdminDto)
        {
            if (ModelState.IsValid)
            {
                CreateAdmin command = new CreateAdmin(addAdminDto);
                try
                {
                    var result = _messages.Dispatch(command);
                    return FromResult(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

        /// <summary
        /// Get user
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/users/{username}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ConfirmUser(string username)
        {
            if (ModelState.IsValid)
            {
                GetUser query = new GetUser(username);
                try
                {
                    var result = _messages.Dispatch(query);
                    return Ok(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

        /// <summary
        /// Get user
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>
 
        [HttpGet("student-enrollment/api/account/{username}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetAccountDetails(string username)
        {
            if (ModelState.IsValid)
            {
                GetUser query = new GetUser(username);
                try
                {
                    var result = _messages.Dispatch(query);
                    return Ok(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }
    }
}