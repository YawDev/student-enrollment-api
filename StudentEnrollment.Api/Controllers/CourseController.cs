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

namespace StudentEnrollment.API.Controllers
{
    [ApiController]
    public class CourseController : BaseController
    {
        private readonly ILogger<CourseController> _logger;
        private readonly Message _messages;
        public CourseController(ILogger<CourseController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }

        /// <summary>
        /// Creates a Course.
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  
        ///     POST /Add Course  
        ///     {
        ///        "name": "MAT 00",
        ///        "title": "string",
        ///        "abbreviation": "MAT",
        ///        "section": "01",
        ///        "courseNumber": "string",
        ///        "instructorName": "User",
        ///        "department": "Mathematics",
        ///        "credits": 0,
        ///        "startTime": "9:00am",
        ///        "endTime": "9:00am",
        ///        "remainingSlots": 0,
        ///        "capacity": 0
        ///     }
        ///
        ///</remarks>
        ///<response code="201">Returns the newly created item</response>
        ///<response code="400">If the item is null</response>
        [HttpPost("student-enrollment/api/courses/add")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 204)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddCourse([FromBody] SaveCourseDto saveCourseDto)
        {
            if (ModelState.IsValid)
            {
                AddCourse command = new AddCourse(saveCourseDto);
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


        [HttpPost("student-enrollment/api/upload/courses/{userid}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 204)]
        [ProducesResponseType(typeof(Envelope), 400)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult UploadCourses(List<UploadCourseDto> UploadCourseDtos, string userid)
        {
            if (ModelState.IsValid)
            {
                UploadCourseCommand command = new UploadCourseCommand(UploadCourseDtos,userid);
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

        /// <summary>
        /// Get Upload Logs for Admin
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/upload/logs/{userid}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetUploadLogs(string userid)
        {
            if (ModelState.IsValid)
            {
                GetUploadLogs query = new GetUploadLogs(userid);
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

        /// <summary>
        /// Get Upload Logs for Admin
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/upload/logs/errors/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetUploadLogErrors(Guid id)
        {
            if (ModelState.IsValid)
            {
                GetLogErrors query = new GetLogErrors(id);
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


        

        [HttpPut("student-enrollment/api/courses/update/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 204)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult UpdateCourse(Guid id, [FromBody] SaveCourseDto saveCourseDto)
        {
            if (ModelState.IsValid)
            {
                UpdateCourse command = new UpdateCourse(saveCourseDto,id);
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

        [HttpDelete("student-enrollment/api/courses/delete/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 204)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult DeleteCourse(Guid id)
        {
            if (ModelState.IsValid)
            {
                DeleteCourse command = new DeleteCourse(id);
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


        
        /// <summary>
        /// Get all Courses
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/courses")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetCourses()
        {
            if (ModelState.IsValid)
            {
                GetCourses query = new GetCourses();
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

        /// <summary>
        /// Get Course by Name and Section
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/courses/{name}/{section}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetCourseBySection(string name, string section)
        {
            if (ModelState.IsValid)
            {
                GetCourseByNameAndSection query = new GetCourseByNameAndSection(name,section);
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


        /// <summary>
        /// Get Course by Id
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/courses/{Id}/details")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetCourseById(Guid Id)
        {
            if (ModelState.IsValid)
            {
                GetCoursesById query = new GetCoursesById(Id);
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



        /// <summary>
        /// Get all Courses by name
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/courses/{coursename}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetCoursesByName(string coursename)
        {
            if (ModelState.IsValid)
            {
                GetCoursesByName query = new GetCoursesByName(coursename);
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

        /// <summary>
        /// Get all Courses by Department
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/{department}/courses")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetCoursesByDepartment(string department)
        {
            if (ModelState.IsValid)
            {
                GetCoursesByDepartment query = new GetCoursesByDepartment(department);
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