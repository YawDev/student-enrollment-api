using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.Core;
using StudentEnrollment.Core.Commands;
using StudentEnrollment.Core.Dtos;
using StudentEnrollment.Core.Exceptions;
using StudentEnrollment.Core.Queries;
using StudentEnrollment.Core.Courses.Commands;
using StudentEnrollment.Core.Admin.Commands.Queries;
using StudentEnrollment.Core.Admin.Queries;
using Microsoft.AspNetCore.Http;
using StudentEnrollment.API.Controllers;
using StudentEnrollment.Core.Users.Queries;

namespace StudentEnrollment.Api.Controllers
{
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly Message _messages;
        public AdminController(ILogger<AdminController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }


        /// <summary
        /// Get Admin Details
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/admin-portal/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetDetails(string id)
        {
            if (ModelState.IsValid)
            {
                GetAdminDetails query = new GetAdminDetails(id);
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