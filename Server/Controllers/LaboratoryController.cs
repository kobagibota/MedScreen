using BaseLibrary.Dtos;
using BaseLibrary.Extentions;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryController : ControllerBase
    {
        #region Private Members

        private readonly ILaboratoryService _laboratoryService;

        #endregion Private Members

        #region Constructor

        public LaboratoryController(ILaboratoryService laboratoryService)
        {
            _laboratoryService = laboratoryService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lab = await _laboratoryService.GetAll();
            return Ok(lab);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _laboratoryService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LaboratoryDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var labResponse = await _laboratoryService.Add(request);

                if (labResponse.Success)
                {
                    return Ok(labResponse);
                }

                return BadRequest(labResponse.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LaboratoryDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var labResponse = await _laboratoryService.Update(id, request);
                if (labResponse.Success)
                {
                    return Ok(labResponse);
                }
                return BadRequest(labResponse.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] LabStatus status)
        {
            try
            {
                var labResponse = await _laboratoryService.ChangeStatus(id, status);
                if (labResponse.Success)
                {
                    return Ok(labResponse);
                }
                return BadRequest(labResponse.Message);
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
                var labResponse = await _laboratoryService.Delete(id);

                if (!labResponse.Success)
                {
                    return BadRequest(labResponse.Message);
                }

                return Ok(labResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
