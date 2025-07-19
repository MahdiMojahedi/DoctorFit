using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramDayMovementController : ControllerBase
    {
        private readonly IServiceProgramDayMovement _programDayMovementService;

        public ProgramDayMovementController(IServiceProgramDayMovement programDayMovementService)
        {
            _programDayMovementService = programDayMovementService;
        }

        [HttpGet]
        public ActionResult<List<ProgramDayMovementDto>> GetAll()
        {
            var list = _programDayMovementService.GetList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<ProgramDayMovementDto>> GetById(int id)
        {
            var item = _programDayMovementService.FindByIDs(new List<int> { id });
            if (item == null || !item.Data.Any())
                return NotFound();

            return Ok(new BaseDto<ProgramDayMovementDto>(true, new List<string>(), item.Data.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<ProgramDayMovementDto>> Create(ProgramDayMovementDto dto)
        {
            var result = _programDayMovementService.Add(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.ProgramDayMovementID }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<ProgramDayMovementDto>> Update(int id, ProgramDayMovementDto dto)
        {
            if (id != dto.ProgramDayMovementID)
                return BadRequest("Id mismatch");

            var result = _programDayMovementService.Edit(dto);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<ProgramDayMovementDto>> Delete(int id)
        {
            var result = _programDayMovementService.Remove(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }

}
