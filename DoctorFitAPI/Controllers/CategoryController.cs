using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceCategory _categoryService;

        public CategoryController(IServiceCategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<BaseListItemDto<CategoryDto>> GetAll()
        {
            var list = _categoryService.GetList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<CategoryDto>> GetById(int id)
        {
            var item = _categoryService.FindByIDs(new List<int> { id });
            if (item == null || !item.Data.Any())
                return NotFound();

            return Ok(new BaseDto<CategoryDto>(true, new List<string>(), item.Data.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<CategoryDto>> Create(CategoryDto dto)
        {
            var result = _categoryService.Add(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.CategoryID }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<CategoryDto>> Update(int id, CategoryDto dto)
        {
            if (id != dto.CategoryID)
                return BadRequest("Id mismatch");

            var result = _categoryService.Edit(dto);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<CategoryDto>> Delete(int id)
        {
            var result = _categoryService.Remove(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }
}
