using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IServiceMovements _movementService;

        public MovementController(IServiceMovements movementService)
        {
            _movementService = movementService;
        }

        [HttpGet]
        public ActionResult<List<MovementDto>> GetAll()
        {
            var list = _movementService.GetList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<MovementDto>> GetById(int id)
        {
            var item = _movementService.FindByIDs(new List<int> { id });
            if (item == null || !item.Data.Any())
                return NotFound();

            return Ok(new BaseDto<MovementDto>(true, new List<string>(), item.Data.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<MovementDto>> Create(MovementDto dto)
        {
            var result = _movementService.Add(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.MovementID }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<MovementDto>> Update(int id, MovementDto dto)
        {
            if (id != dto.MovementID)
                return BadRequest("Id mismatch");

            var result = _movementService.Edit(dto);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<MovementDto>> Delete(int id)
        {
            var result = _movementService.Remove(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }

}
