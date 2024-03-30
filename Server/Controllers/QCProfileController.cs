using BaseLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Extensions;
using Server.Services;

namespace Server.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class QCProfileController : ControllerBase
    {
        #region Private Members

        private readonly IQCProfileService _qcProfileService;

        #endregion Private Members

        #region Constructor

        public QCProfileController(IQCProfileService qcProfileService)
        {
            _qcProfileService = qcProfileService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string userId = HttpContext.GetUserId();
            var response = await _qcProfileService.GetListByUserId(userId);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _qcProfileService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QCProfileDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _qcProfileService.Add(request);

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
        public async Task<IActionResult> Update([FromBody] QCProfileDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _qcProfileService.Update(request);
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
                var response = await _qcProfileService.Delete(id);

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

        #endregion
    }
}
