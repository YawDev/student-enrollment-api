using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.Core;
using StudentEnrollment.Core.Commands;
using StudentEnrollment.Core.Dtos;
using StudentEnrollment.Core.Exceptions;
using StudentEnrollment.Core.Queries;
using StudentEnrollment.Core.Students.Commands;
using StudentEnrollment.Core.Students.Queries;

namespace StudentEnrollment.API.Controllers
{
    [ApiController]

    public class StudentController : BaseController
    {
        private readonly ILogger<StudentController> _logger;
        private readonly Message _messages;
        public StudentController(ILogger<StudentController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }

        [HttpPost("student-enrollment/api/students/add")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddStudent([FromBody] AddStudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                AddStudent command = new AddStudent(studentDto);
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
        /// Get all Students
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/students")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetStudents()
        {
            if (ModelState.IsValid)
            {
                GetStudents query = new GetStudents();
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
        /// Get Student Details
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/student-account/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetDetails(string id)
        {
            if (ModelState.IsValid)
            {
                GetStudentDetails query = new GetStudentDetails(id);
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
        /// Enroll In Course
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpPost("student-enrollment/api/courses-enroll/{Id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult EnrollInCourse([FromBody] EnrollCourseDto enrollCourseDto, Guid Id)
        {
            if (ModelState.IsValid)
            {
                EnrollCourse command = new EnrollCourse(enrollCourseDto, Id);
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
        /// Sync Student Data
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpPost("student-enrollment/api/sync/student-details/{Id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult SyncStudentDetails(Guid Id)
        {
            if (ModelState.IsValid)
            {
                SyncStudentData command = new SyncStudentData(Id);
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
        /// Get Enrollments for Student
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/my-enrollments/{Id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult StudentEnrollments(Guid Id)
        {
            if (ModelState.IsValid)
            {
                GetEnrollmentsOfStudent query = new GetEnrollmentsOfStudent(Id);
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
        /// Get Last Sync for Student
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/sync-logs/{Id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetLastSyncLog(Guid Id)
        {
            if (ModelState.IsValid)
            {
                GetLastStudentSyncLog query = new GetLastStudentSyncLog(Id);
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