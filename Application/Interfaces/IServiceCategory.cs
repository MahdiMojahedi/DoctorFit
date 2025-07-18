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
    public interface IServiceCategory
    {
        BaseDto<CategoryDto> Add(CategoryDto categoryDto);
        public BaseListItemDto<CategoryDto> GetList();
        public BaseListItemDto<CategoryDto> FindByIDs(List<int> IDs);
        BaseDto<CategoryDto> Edit(CategoryDto categoryDto);
        BaseDto Remove(int id);
    }


    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
