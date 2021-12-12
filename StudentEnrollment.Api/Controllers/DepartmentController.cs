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

    public class DepartmentController : BaseController
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly Message _messages;
        public DepartmentController(ILogger<DepartmentController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }

        /// <summary>
        /// Creates a Department.
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  
        ///     POST /Add Deparment  
        ///     {
        ///         "title": "string"
        ///     }
        ///
        ///</remarks>
        ///<response code="201">Returns the newly created item</response>
        ///<response code="400">If the item is null</response>
        [HttpPost("student-enrollment/api/departments/add")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddDepartment([FromBody] AddDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                AddDepartment command = new AddDepartment(departmentDto);
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

       


        [HttpGet("student-enrollment/api/departments/details/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetDepartmentDetails(Guid id)
        {
            if (ModelState.IsValid)
            {
                GetDepartmentById query = new GetDepartmentById(id);
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

        [HttpDelete("student-enrollment/api/departments/delete/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult RemoveDepartment(Guid id)
        {
            if (ModelState.IsValid)
            {
                DeleteDepartment command = new DeleteDepartment(id);
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
        /// Get all Departments
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/departments")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetDepartments()
        {
            if (ModelState.IsValid)
            {
                GetDepartments query = new GetDepartments();
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