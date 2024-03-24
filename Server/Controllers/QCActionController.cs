using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QCActionController : ControllerBase
    {
        #region Private Members

        private readonly IQCActionService _qcActionService;

        #endregion Private Members

        #region Constructor

        public QCActionController(IQCActionService qcActionService)
        {
            _qcActionService = qcActionService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var qcAction = await _qcActionService.GetAll();
            return Ok(qcAction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _qcActionService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QCAction request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var qcActionResponse = await _qcActionService.Add(request);

                if (qcActionResponse.Success)
                {
                    return Ok(qcActionResponse);
                }

                return BadRequest(qcActionResponse.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QCAction request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var qcActionResponse = await _qcActionService.Update(id, request);
                if (qcActionResponse.Success)
                {
                    return Ok(qcActionResponse);
                }
                return BadRequest(qcActionResponse.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var qcActionResponse = await _qcActionService.Delete(id);

                if (!qcActionResponse.Success)
                {
                    return BadRequest(qcActionResponse.Message);
                }

                return Ok(qcActionResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
