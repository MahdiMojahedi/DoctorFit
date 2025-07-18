using Application.Dtos;
using Application.Services;
using Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceProgramDays
    {
        BaseDto<ProgramDaysDto> Add(ProgramDaysDto programDaysDto);
        public List<ProgramDaysDto> GetList();
        public BaseListItemDto<ProgramDaysDto> FindByIDs(List<int> IDs);
        BaseDto<ProgramDaysDto> Edit(ProgramDaysDto programDaysDto);
        BaseDto Remove(int id);
    }

    public class ProgramDaysDto
    {
        public int ProgramDaysID { get; set; }
        public EnumWeekDay DayOfWeek { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public bool IsRestDay { get; set; }
        public int FK_ProgramID { get; set; }
        public Program Program { get; set; }
        public ICollection<ProgramDayMovement> ProgramDayMovement { get; set; }
    }
}
