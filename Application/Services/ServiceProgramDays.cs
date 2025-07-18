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
    public class ServiceProgramDay : IServiceProgramDays
    {
        private readonly IDataBaseContext DbContext;
        private readonly IMapper Mapper;

        public ServiceProgramDay(IDataBaseContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        public BaseDto<ProgramDaysDto> Add(ProgramDaysDto dto)
        {
            var entity = Mapper.Map<ProgramDays>(dto);
            DbContext.ProgramDays.Add(entity);

            if (DbContext.SaveChanges() > 0)
            {
                var savedDto = Mapper.Map<ProgramDaysDto>(entity);
                return new BaseDto<ProgramDaysDto>(true, new List<string> { "ProgramDay added successfully." }, savedDto);
            }

            return new BaseDto<ProgramDaysDto>(false, new List<string> { "Failed to add ProgramDay." }, null);
        }

        public BaseDto<ProgramDaysDto> Edit(ProgramDaysDto dto)
        {
            var entity = DbContext.ProgramDays.Find(dto.ProgramDaysID);
            if (entity == null)
            {
                return new BaseDto<ProgramDaysDto>(false, new List<string> { "ProgramDay not found." }, null);
            }

            Mapper.Map(dto, entity);
            DbContext.SaveChanges();

            var updatedDto = Mapper.Map<ProgramDaysDto>(entity);

            return new BaseDto<ProgramDaysDto>(true, new List<string> { "ProgramDay updated successfully." }, updatedDto);
        }

        public BaseListItemDto<ProgramDaysDto> FindByIDs(List<int> IDs)
        {
            if (IDs == null || !IDs.Any())
                return new BaseListItemDto<ProgramDaysDto>(Enumerable.Empty<ProgramDaysDto>());

            var entities = DbContext.ProgramDays.Where(pd => IDs.Contains(pd.ProgramDaysID)).ToList();

            var dtos = Mapper.Map<List<ProgramDaysDto>>(entities);

            return new BaseListItemDto<ProgramDaysDto>(dtos);
        }

        public List<ProgramDaysDto> GetList()
        {
            var entities = DbContext.ProgramDays.ToList();

            return Mapper.Map<List<ProgramDaysDto>>(entities);
        }

        public BaseDto Remove(int id)
        {
            var entity = DbContext.ProgramDays.Find(id);

            if (entity == null)
                return new BaseDto(false, new List<string> { "ProgramDay not found." });

            DbContext.ProgramDays.Remove(entity);
            DbContext.SaveChanges();

            return new BaseDto(true, new List<string> { "ProgramDay deleted successfully." });
        }
    }



}
