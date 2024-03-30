using BaseLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Private Members

        private readonly ICategoryService _categoryService;

        #endregion Private Members

        #region Constructor

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _categoryService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _categoryService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoryResponse = await _categoryService.Add(request);

                if (categoryResponse.Success)
                {
                    return Ok(categoryResponse);
                }

                return BadRequest(categoryResponse.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoryResponse = await _categoryService.Update(id, request);
                if (categoryResponse.Success)
                {
                    return Ok(categoryResponse);
                }
                return BadRequest(categoryResponse.Message);
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
                var categoryResponse = await _categoryService.Delete(id);

                if (!categoryResponse.Success)
                {
                    return BadRequest(categoryResponse.Message);
                }

                return Ok(categoryResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        #endregion
    }
}
