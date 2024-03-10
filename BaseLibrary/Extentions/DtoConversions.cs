using BaseLibrary.Dtos;
using BaseLibrary.Entities;

namespace BaseLibrary.Extentions
{
    public static class DtoConversions
    {
        public static CategoryDto ConvertToDto(this Category category)
        {
            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
            return categoryDto;
        }

        public static IEnumerable<CategoryDto> ConvertToDto(this IEnumerable<Category> categories)
        {
            return categories.Select(ConvertToDto);
        }
    }
}
