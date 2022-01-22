using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.API.Controllers;
using StudentEnrollment.Core;
using StudentEnrollment.Core.Exceptions;
using StudentEnrollment.Core.Files;

namespace StudentEnrollment.Api.Controllers
{
    public class FilesController  : BaseController
    {
        private readonly ILogger<FilesController> _logger;
        private readonly Message _messages;
        public FilesController(ILogger<FilesController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }

         /// <summary>
        /// Get files for Admin
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/files/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetFiles(string id)
        {
            if (ModelState.IsValid)
            {
                GetFilesByUploader query = new GetFilesByUploader(id);
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
        /// Get file by id
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("student-enrollment/api/filecontent/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetFileById(Guid id)
        {
            if (ModelState.IsValid)
            {
                GetFileById query = new GetFileById(id);
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
        /// Delete File
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpDelete("student-enrollment/api/uploads/delete/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult DeleteUploadedFile(Guid id)
        {
            if (ModelState.IsValid)
            {
                DeleteFile command = new DeleteFile(id);
                try
                {
                    var result = _messages.Dispatch(command);
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