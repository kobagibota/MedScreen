using BaseLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StrainController : ControllerBase
    {
        #region Private Members

        private readonly IStrainService _strainService;

        #endregion Private Members

        #region Constructor

        public StrainController(IStrainService strainService)
        {
            _strainService = strainService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _strainService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _strainService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StrainDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _strainService.Add(request);

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
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StrainDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _strainService.Update(id, request);
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
                var response = await _strainService.Delete(id);

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
