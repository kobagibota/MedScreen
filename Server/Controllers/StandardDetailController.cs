using BaseLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class StandardDetailController : ControllerBase
    {
        #region Private Members

        private readonly IStandardDetailService _standardDetailService;

        #endregion Private Members

        #region Constructor

        public StandardDetailController(IStandardDetailService standardDetailService)
        {
            _standardDetailService = standardDetailService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("standard/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var response = await _standardDetailService.GetListByStandardId(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _standardDetailService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StandardDetailDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _standardDetailService.Add(request);

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
        public async Task<IActionResult> Update([FromBody] StandardDetailDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _standardDetailService.Update(request);
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
                var response = await _standardDetailService.Delete(id);

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
