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
    public interface IServiceMovements
    {
        BaseDto<MovementDto> Add(MovementDto movementDto);
        public List<MovementDto> GetList();
        public BaseListItemDto<MovementDto> FindByIDs(List<int> IDs);
        BaseDto<MovementDto> Edit(MovementDto movementDto);
        BaseDto Remove(int id);

    }
    public class MovementDto
    {
        public long MovementID { get; set; }
        public string Title { get; set; }
        public string GifFilePath { get; set; }
    }
}
