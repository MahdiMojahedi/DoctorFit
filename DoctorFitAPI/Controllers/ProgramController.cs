using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly IServiceProgram _programService;

        public ProgramController(IServiceProgram programService)
        {
            _programService = programService;
        }

        [HttpGet]
        public ActionResult<List<ProgramDto>> GetAll()
        {
            var list = _programService.GetList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<ProgramDto>> GetById(int id)
        {
            var item = _programService.FindByIDs(new List<int> { id });
            if (item == null || !item.Data.Any())
                return NotFound();

            return Ok(new BaseDto<ProgramDto>(true, new List<string>(), item.Data.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<ProgramDto>> Create(ProgramDto dto)
        {
            var result = _programService.Add(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.ProgramID }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<ProgramDto>> Update(int id, ProgramDto dto)
        {
            if (id != dto.ProgramID)
                return BadRequest("Id mismatch");

            var result = _programService.Edit(dto);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<ProgramDto>> Delete(int id)
        {
            var result = _programService.Remove(id);
            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }

}
