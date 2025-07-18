using Application.Dtos;
using Application.Services;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceProgramDayMovement
    {
        BaseDto<ProgramDayMovementDto> Add(ProgramDayMovementDto programDayMovementDto);
        public List<ProgramDayMovementDto> GetList();
        public BaseListItemDto<ProgramDayMovementDto> FindByIDs(List<int> IDs);
        BaseDto<ProgramDayMovementDto> Edit(ProgramDayMovementDto programDayMovementDto);
        BaseDto Remove(int id);
    }

    public class ProgramDayMovementDto
    {
        public int ProgramDayMovementID { get; set; }
        public string OrderInDay { get; set; }
        public Int16 Sets { get; set; }
        public Int16 Reps { get; set; }
        public int DurationSeconds { get; set; }
        public int FK_ProgramDaysID { get; set; }
        public ProgramDays ProgramDays { get; set; }
        public int FK_MovementID { get; set; }
        public Movement Movement { get; set; }
    }
}
