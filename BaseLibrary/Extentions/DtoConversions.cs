using BaseLibrary.Dtos;
using BaseLibrary.Entities;

namespace BaseLibrary.Extentions
{
    public static class DtoConversions
    {
        #region Category

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

        #endregion

        #region Method

        public static MethodDto ConvertToDto(this Method method)
        {
            var methodDto = new MethodDto()
            {
                Id = method.Id,
                MethodName = method.MethodName
            };
            return methodDto;
        }

        public static IEnumerable<MethodDto> ConvertToDto(this IEnumerable<Method> standards)
        {
            return standards.Select(ConvertToDto);
        }

        #endregion

        #region Standard
        public static StandardDto ConvertToDto(this Standard standard)
        {
            var standardDto = new StandardDto()
            {
                Id = standard.Id,
                StandardName = standard.StandardName
            };
            return standardDto;
        }

        public static IEnumerable<StandardDto> ConvertToDto(this IEnumerable<Standard> strainGroups)
        {
            return strainGroups.Select(ConvertToDto);
        }

        #endregion

        #region StrainGroup

        public static StrainGroupDto ConvertToDto(this StrainGroup strainGroup)
        {
            var strainGroupDto = new StrainGroupDto()
            {
                Id = strainGroup.Id,
                GroupName = strainGroup.GroupName
            };
            return strainGroupDto;
        }

        public static IEnumerable<StrainGroupDto> ConvertToDto(this IEnumerable<StrainGroup> strainGroups)
        {
            return strainGroups.Select(ConvertToDto);
        }

        #endregion

        #region Strain

        public static StrainDto ConvertToDto(this Strain strain)
        {
            var strainDto = new StrainDto()
            {
                Id = strain.Id,
                StrainName = strain.StrainName,
                GroupId = strain.GroupId,
                GroupName = strain.StrainGroup?.GroupName
            };
            return strainDto;
        }

        public static IEnumerable<StrainDto> ConvertToDto(this IEnumerable<Strain> strains)
        {
            return strains.Select(ConvertToDto);
        }

        #endregion

        #region TestType

        public static TestTypeDto ConvertToDto(this TestType testType)
        {
            var testTypeDto = new TestTypeDto()
            {
                Id = testType.Id,
                TypeName = testType.TypeName,
                Unit = testType.Unit
            };
            return testTypeDto;
        }

        public static IEnumerable<TestTypeDto> ConvertToDto(this IEnumerable<TestType> testTypes)
        {
            return testTypes.Select(ConvertToDto);
        }

        #endregion

        #region TestQC

        public static TestQCDto ConvertToDto(this TestQC testQC)
        {
            var testQCDto = new TestQCDto()
            {
                Id = testQC.Id,
                TestQCName = testQC.TestQCName,
                TestTypeId = testQC.TestTypeId,
                TestTypeName = testQC.TestType.TypeName
            };
            return testQCDto;
        }

        public static IEnumerable<TestQCDto> ConvertToDto(this IEnumerable<TestQC> testQCs)
        {
            return testQCs.Select(ConvertToDto);
        }

        #endregion

        #region Supply

        public static SupplyDto ConvertToDto(this Supply supply)
        {
            var supplyDto = new SupplyDto()
            {
                Id = supply.Id,
                SupplyName = supply.SupplyName,
                MethodId = supply.MethodId,
                MethodName = supply.Method.MethodName
            };
            return supplyDto;
        }

        public static IEnumerable<SupplyDto> ConvertToDto(this IEnumerable<Supply> supplies)
        {
            return supplies.Select(ConvertToDto);
        }

        #endregion

        #region LotTest

        public static LotTestDto ConvertToDto(this LotTest lotTest)
        {
            var supplyDto = new LotTestDto()
            {
                Id = lotTest.Id,
                LotNumber = lotTest.LotNumber,
                ExpDate = lotTest.ExpDate,
                Default = lotTest.Default,
                TestQCId = lotTest.TestQCId,
                TestQCName = lotTest.TestQC.TestQCName
            };
            return supplyDto;
        }

        public static IEnumerable<LotTestDto> ConvertToDto(this IEnumerable<LotTest> lotTests)
        {
            return lotTests.Select(ConvertToDto);
        }

        #endregion

        #region Category



        #endregion
    }
}
