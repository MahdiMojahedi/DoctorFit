using Application.Dtos;
using Application.Services;
using Entities;
using IDP.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceProgram
    {
        BaseDto<ProgramDto> Add(ProgramDto programDto);
        public List<ProgramDto> GetList();
        public BaseListItemDto<ProgramDto> FindByIDs(List<int> IDs);
        BaseDto<ProgramDto> Edit(ProgramDto programDto);
        BaseDto Remove(int id);
    }
    public class ProgramDto
    {
        public int ProgramID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_StudentID { get; set; }
        public ApplicationUser Student { get; set; }
        public int FK_CoachID { get; set; }
        public ApplicationUser Coach { get; set; }
        public ICollection<ProgramDays> ProgramDays { get; set; }
    }
}
