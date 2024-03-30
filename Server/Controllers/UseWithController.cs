using BaseLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UseWithController : ControllerBase
    {
        #region Private Members

        private readonly IUseWithService _useWithService;

        #endregion Private Members

        #region Constructor

        public UseWithController(IUseWithService useWithService)
        {
            _useWithService = useWithService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("qc/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var response = await _useWithService.GetByQCId(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _useWithService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UseWithDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _useWithService.Add(request);

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
        public async Task<IActionResult> Update([FromBody] UseWithDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _useWithService.Update(request);
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
                var response = await _useWithService.Delete(id);

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
