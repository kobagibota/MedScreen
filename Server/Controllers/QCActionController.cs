using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QCActionController : ControllerBase
    {
        #region Private Members

        private readonly IQCActionServices _qcActionServices;

        #endregion Private Members

        #region Constructor

        public QCActionController(IQCActionServices qcActionServices)
        {
            _qcActionServices = qcActionServices;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var qcAction = await _qcActionServices.GetAll();
            return Ok(qcAction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _qcActionServices.GetById(id);

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

                var qcActionResponse = await _qcActionServices.Add(request);

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

                var qcActionResponse = await _qcActionServices.Update(id, request);
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
                var qcActionResponse = await _qcActionServices.Delete(id);

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
