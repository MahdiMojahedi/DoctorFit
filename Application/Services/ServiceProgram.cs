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
    public class ServiceProgram : IServiceProgram
    {
        private readonly IDataBaseContext DbContext;
        private readonly IMapper Mapper;

        public ServiceProgram(IDataBaseContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        public BaseDto<ProgramDto> Add(ProgramDto programDto)
        {
            var program = Mapper.Map<Program>(programDto);
            DbContext.Programs.Add(program);

            if (DbContext.SaveChanges() > 0)
            {
                var savedDto = Mapper.Map<ProgramDto>(program);
                return new BaseDto<ProgramDto>(
                    IsSuccess: true,
                    Message: new List<string> { "Program added successfully." },
                    Data: savedDto
                );
            }

            return new BaseDto<ProgramDto>(
                IsSuccess: false,
                Message: new List<string> { "Failed to add program." },
                Data: null
            );
        }

        public BaseDto<ProgramDto> Edit(ProgramDto programDto)
        {
            var program = DbContext.Programs.Find(programDto.ProgramID);
            if (program == null)
            {
                return new BaseDto<ProgramDto>(
                    IsSuccess: false,
                    Message: new List<string> { "Program not found." },
                    Data: null
                );
            }

            Mapper.Map(programDto, program);
            DbContext.SaveChanges();

            var updatedDto = Mapper.Map<ProgramDto>(program);

            return new BaseDto<ProgramDto>(
                IsSuccess: true,
                Message: new List<string> { "Program updated successfully." },
                Data: updatedDto
            );
        }

        public BaseListItemDto<ProgramDto> FindByIDs(List<int> IDs)
        {
            if (IDs == null || !IDs.Any())
                return new BaseListItemDto<ProgramDto>(Enumerable.Empty<ProgramDto>());

            var programs = DbContext.Programs
                .Where(p => IDs.Contains(p.ProgramID))
                .ToList();

            var dtos = Mapper.Map<List<ProgramDto>>(programs);

            return new BaseListItemDto<ProgramDto>(dtos);
        }

        public List<ProgramDto> GetList()
        {
            var programs = DbContext.Programs.ToList();
            return Mapper.Map<List<ProgramDto>>(programs);
        }

        public BaseDto Remove(int id)
        {
            var program = DbContext.Programs.Find(id);
            if (program == null)
            {
                return new BaseDto(
                    IsSuccess: false,
                    Message: new List<string> { "Program not found." }
                );
            }

            DbContext.Programs.Remove(program);
            DbContext.SaveChanges();

            var removedDto = Mapper.Map<ProgramDto>(program);

            return new BaseDto(
                IsSuccess: true,
                Message: new List<string> { "Program deleted successfully." }
            );
        }

        // For ProgramDayMovement-related methods, I suggest a separate service, 
        // but if you want here, implement similarly, changing DbContext.Programs to DbContext.ProgramDayMovements, etc.
    }



}
