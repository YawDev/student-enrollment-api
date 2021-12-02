using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.API.Controllers;
using StudentEnrollment.Core;
using StudentEnrollment.Core.Enrollments.Queries;
using StudentEnrollment.Core.Exceptions;

namespace StudentEnrollment.Api.Controllers
{
    public class EnrollmentsController : BaseController
    {
        private readonly ILogger<EnrollmentsController> _logger;
        private readonly Message _messages;
        public EnrollmentsController(ILogger<EnrollmentsController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }


               /// <summary
        /// Get Enrollment by Id
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/enrollment/{Id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetEnrollment(Guid Id)
        {
            if (ModelState.IsValid)
            {
                GetEnrollmentById query = new GetEnrollmentById(Id);
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