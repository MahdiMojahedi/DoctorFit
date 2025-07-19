using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramDaysController : ControllerBase
    {
        private readonly IServiceProgramDays _programDaysService;

        public ProgramDaysController(IServiceProgramDays programDaysService)
        {
            _programDaysService = programDaysService;
        }

        [HttpGet]
        public ActionResult<List<ProgramDaysDto>> GetAll()
        {
            var list = _programDaysService.GetList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<ProgramDaysDto>> GetById(int id)
        {
            var item = _programDaysService.FindByIDs(new List<int> { id });
            if (item == null || !item.Data.Any())
                return NotFound();

            return Ok(new BaseDto<ProgramDaysDto>(true, new List<string>(), item.Data.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<ProgramDaysDto>> Create(ProgramDaysDto dto)
        {
            var result = _programDaysService.Add(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.ProgramDaysID }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<ProgramDaysDto>> Update(int id, ProgramDaysDto dto)
        {
            if (id != dto.ProgramDaysID)
                return BadRequest("Id mismatch");

            var result = _programDaysService.Edit(dto);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<ProgramDaysDto>> Delete(int id)
        {
            var result = _programDaysService.Remove(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }

}
