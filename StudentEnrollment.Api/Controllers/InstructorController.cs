using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.Core;
using StudentEnrollment.Core.Commands;
using StudentEnrollment.Core.Courses.Queries;
using StudentEnrollment.Core.Dtos;
using StudentEnrollment.Core.Exceptions;
using StudentEnrollment.Core.Instructors.Commands;
using StudentEnrollment.Core.Queries;

namespace StudentEnrollment.API.Controllers
{
    [ApiController]
    public class InstructorsController : BaseController
    {
        private readonly ILogger<CourseController> _logger;
        private readonly Message _messages;
        public InstructorsController(ILogger<CourseController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }

        [HttpPost("student-enrollment/api/instructors/add")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 204)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddInstructor([FromBody] AddInstructorDto addInstructorDto)
        {
            if (ModelState.IsValid)
            {
                AddInstructor command = new AddInstructor(addInstructorDto);
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
        /// Get all Instructors
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/instructors")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetInstructors()
        {
            if (ModelState.IsValid)
            {
                GetInstructors query = new GetInstructors();
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
        /// Get all Instructors
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/instructors/{department}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetInstructors(string department)
        {
            if (ModelState.IsValid)
            {
                GetInstructorsByDepartment query = new GetInstructorsByDepartment(department);
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
        /// Get Instructor Details
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/instructor-account/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetDetails(string id)
        {
            if (ModelState.IsValid)
            {
                GetInstructorDetails query = new GetInstructorDetails(id);
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
        /// Get Course for Instructor
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/instructor/my-course/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetInstructorCourse(Guid id)
        {
            if (ModelState.IsValid)
            {
                GetCourseDetailsForInstructor query = new GetCourseDetailsForInstructor(id);
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


        [HttpPost("student-enrollment/api/submit-grades")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 204)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult SubmitGrade([FromBody] SubmitGradeDto submitGradeDto)
        {
            if (ModelState.IsValid)
            {
                SubmitGrade command = new SubmitGrade(submitGradeDto);
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

        


    }

}