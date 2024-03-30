using BaseLibrary.Dtos;
using BaseLibrary.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QCController : ControllerBase
    {
        #region Private Members

        private readonly IQCService _qcService;

        #endregion Private Members

        #region Constructor

        public QCController(IQCService qcService)
        {
            _qcService = qcService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("lab/{id}")]
        public async Task<IActionResult> GetAll(int id, [FromQuery] DateOnly qcDate)
        {
            var response = await _qcService.GetByLabId(id, qcDate);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _qcService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QCDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _qcService.Add(request);

                if (response.Success)
                {
                    return Ok(response);
                }

                return BadRequest(response.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QCDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _qcService.Update(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _qcService.Delete(id);

                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        [HttpPut]
        [Route("action/{id}")]
        public async Task<IActionResult> UpdateAction(int id, [FromBody] string action)
        {
            try
            {
                var response = await _qcService.UpdateAction(id, action);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        [HttpPut]
        [Route("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] QCStatus status)
        {
            try
            {
                var response = await _qcService.UpdateStatus(id, status);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        #endregion
    }
}
