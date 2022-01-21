using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}