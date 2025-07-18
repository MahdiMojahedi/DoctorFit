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

    public class ServiceMovement : IServiceMovements
    {
        private readonly IDataBaseContext DbContext;
        private readonly IMapper Mapper;

        public ServiceMovement(IDataBaseContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        public BaseDto<MovementDto> Add(MovementDto movementDto)
        {
            var movement = Mapper.Map<Movement>(movementDto);

            DbContext.Movements.Add(movement);

            if (DbContext.SaveChanges() > 0)
            {
                var savedDto = Mapper.Map<MovementDto>(movement);

                return new BaseDto<MovementDto>(
                    IsSuccess: true,
                    Message: new List<string> { "Movement added successfully." },
                    Data: savedDto
                );
            }

            return new BaseDto<MovementDto>(
                IsSuccess: false,
                Message: new List<string> { "Failed to add movement." },
                Data: null
            );
        }

        public BaseDto<MovementDto> Edit(MovementDto movementDto)
        {
            var movement = DbContext.Movements.Find(movementDto.MovementID);

            if (movement == null)
            {
                return new BaseDto<MovementDto>(
                    IsSuccess: false,
                    Message: new List<string> { "Movement not found." },
                    Data: null
                );
            }

            Mapper.Map(movementDto, movement);
            DbContext.SaveChanges();

            var updatedDto = Mapper.Map<MovementDto>(movement);

            return new BaseDto<MovementDto>(
                IsSuccess: true,
                Message: new List<string> { "Movement updated successfully." },
                Data: updatedDto
            );
        }

        public BaseListItemDto<MovementDto> FindByIDs(List<int> IDs)
        {
            if (IDs == null || !IDs.Any())
            {
                return new BaseListItemDto<MovementDto>(Enumerable.Empty<MovementDto>());
            }

            var movements = DbContext.Movements
                .Where(m => IDs.Contains(m.MovementID))
                .ToList();

            var dtos = Mapper.Map<List<MovementDto>>(movements);

            return new BaseListItemDto<MovementDto>(dtos);
        }

        public List<MovementDto> GetList()
        {
            var movements = DbContext.Movements.ToList();

            return Mapper.Map<List<MovementDto>>(movements);
        }

        public BaseDto Remove(int id)
        {
            var movement = DbContext.Movements.Find(id);

            if (movement == null)
            {
                return new BaseDto(
                    IsSuccess: false,
                    Message: new List<string> { "Movement not found." }
                );
            }

            DbContext.Movements.Remove(movement);
            DbContext.SaveChanges();

            var removedDto = Mapper.Map<MovementDto>(movement);

            return new BaseDto(
                IsSuccess: true,
                Message: new List<string> { "Movement deleted successfully." }
            );
        }

       
    }

}
