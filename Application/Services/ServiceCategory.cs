using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceCategory : IServiceCategory
    {
        private readonly IDataBaseContext DbContext;
        private readonly IMapper Mapper;

        public ServiceCategory(IDataBaseContext baseContext, IMapper mapper)
        {
            this.DbContext = baseContext;
            Mapper = mapper;
        }
        public BaseDto<CategoryDto> Add(CategoryDto categoryDto)
        {
            var category = Mapper.Map<Category>(categoryDto);

            DbContext.Categories.Add(category);

            var result = DbContext.SaveChanges();

            if (result > 0)
            {
                // Map the saved entity back (to get updated fields like ID)
                var savedDto = Mapper.Map<CategoryDto>(category);

                return new BaseDto<CategoryDto>(
                    IsSuccess: true,
                    Message: new List<string> { "Category added successfully." },
                    Data: savedDto
                );
            }

            return new BaseDto<CategoryDto>(
                IsSuccess: false,
                Message: new List<string> { "Failed to add category." },
                Data: null
            );
        }


        public BaseDto<CategoryDto> Edit(CategoryDto categoryDto)
        {
            var category = DbContext.Categories.Find(categoryDto.CategoryID);

            if (category == null)
            {
                return new BaseDto<CategoryDto>(
                    IsSuccess: false,
                    Message: new List<string> { "Category not found." },
                    Data: null
                );
            }


            Mapper.Map(categoryDto, category);

            DbContext.SaveChanges();

            var updatedDto = Mapper.Map<CategoryDto>(category);

            return new BaseDto<CategoryDto>(
                IsSuccess: true,
                Message: new List<string> { "Category updated successfully." },
                Data: updatedDto
            );
        }


        public BaseListItemDto<CategoryDto> FindByIDs(List<int> IDs)
        {
            if (IDs == null || !IDs.Any())
            {
                return new BaseListItemDto<CategoryDto>(Enumerable.Empty<CategoryDto>());
            }

            var categories = DbContext.Categories
                                      .Where(o => IDs.Contains(o.CategoryID))
                                      .ToList();

            var categoryDtos = Mapper.Map<List<CategoryDto>>(categories);

            return new BaseListItemDto<CategoryDto>(categoryDtos);
        }

        public BaseListItemDto<CategoryDto> GetList()
        {
            var categories = DbContext.Categories?.ToList() ?? new List<Category>();

            var categoryDtos = Mapper.Map<List<CategoryDto>>(categories);

            return new BaseListItemDto<CategoryDto>(categoryDtos);
        }


        public BaseDto Remove(int id)
        {
            var category = DbContext.Categories.Find(id);

            if (category == null)
            {
                return new BaseDto(
                    IsSuccess: false,
                    Message: new List<string> { "Category not found." }
                );
            }

            DbContext.Categories.Remove(category);
            DbContext.SaveChanges();

            var deletedDto = Mapper.Map<CategoryDto>(category);

            return new BaseDto(
                IsSuccess: true,
                Message: new List<string> { "Category deleted successfully." }
            );
        }


    }
}
