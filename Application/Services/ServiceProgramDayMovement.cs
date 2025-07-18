using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceProgramDayMovement : IServiceProgramDayMovement
    {
        private readonly IDataBaseContext DbContext;
        private readonly IMapper Mapper;

        public ServiceProgramDayMovement(IDataBaseContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        public BaseDto<ProgramDayMovementDto> Add(ProgramDayMovementDto dto)
        {
            var entity = Mapper.Map<ProgramDayMovement>(dto);
            DbContext.ProgramDayMovements.Add(entity);

            if (DbContext.SaveChanges() > 0)
            {
                var savedDto = Mapper.Map<ProgramDayMovementDto>(entity);
                return new BaseDto<ProgramDayMovementDto>(
                    true,
                    new List<string> { "ProgramDayMovement added successfully." },
                    savedDto);
            }

            return new BaseDto<ProgramDayMovementDto>(
                false,
                new List<string> { "Failed to add ProgramDayMovement." },
                null);
        }

        public BaseDto<ProgramDayMovementDto> Edit(ProgramDayMovementDto dto)
        {
            var entity = DbContext.ProgramDayMovements.Find(dto.ProgramDayMovementID);
            if (entity == null)
            {
                return new BaseDto<ProgramDayMovementDto>(
                    false,
                    new List<string> { "ProgramDayMovement not found." },
                    null);
            }

            Mapper.Map(dto, entity);
            DbContext.SaveChanges();

            var updatedDto = Mapper.Map<ProgramDayMovementDto>(entity);

            return new BaseDto<ProgramDayMovementDto>(
                true,
                new List<string> { "ProgramDayMovement updated successfully." },
                updatedDto);
        }

        public BaseListItemDto<ProgramDayMovementDto> FindByIDs(List<int> IDs)
        {
            if (IDs == null || !IDs.Any())
                return new BaseListItemDto<ProgramDayMovementDto>(Enumerable.Empty<ProgramDayMovementDto>());

            var entities = DbContext.ProgramDayMovements
                .Where(pdm => IDs.Contains(pdm.ProgramDayMovementID))
                .ToList();

            var dtos = Mapper.Map<List<ProgramDayMovementDto>>(entities);

            return new BaseListItemDto<ProgramDayMovementDto>(dtos);
        }

        public List<ProgramDayMovementDto> GetList()
        {
            var entities = DbContext.ProgramDayMovements.ToList();

            return Mapper.Map<List<ProgramDayMovementDto>>(entities);
        }

        public BaseDto Remove(int id)
        {
            var entity = DbContext.ProgramDayMovements.Find(id);

            if (entity == null)
            {
                return new BaseDto(
                    false,
                    new List<string> { "ProgramDayMovement not found." });
            }

            DbContext.ProgramDayMovements.Remove(entity);
            DbContext.SaveChanges();

            return new BaseDto(
                true,
                new List<string> { "ProgramDayMovement deleted successfully." });
        }
    }


}
