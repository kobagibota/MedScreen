using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using System.Collections.Generic;

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

        public static StrainDto ConvertToDto(this Strain strain, List<StrainType> strainTypes)
        {
            var strainDto = new StrainDto()
            {
                Id = strain.Id,
                GroupId = strain.GroupId,
                GroupName = strain.StrainGroup.GroupName,
                StrainName = strain.StrainName,
                TypeTest = strainTypes.FirstOrDefault(st => st.StrainId == strain.Id && st.CategoryId == 1)?.InUse == true,
                TypeID = strainTypes.FirstOrDefault(st => st.StrainId == strain.Id && st.CategoryId == 2)?.InUse == true,
                TypeAST = strainTypes.FirstOrDefault(st => st.StrainId == strain.Id && st.CategoryId == 3)?.InUse == true
            };
            return strainDto;
        }


        public static IEnumerable<StrainDto> ConvertToDto(this IEnumerable<Strain> strains, List<StrainType> strainTypes)
        {
            return strains.Select(strain => strain.ConvertToDto(strainTypes));
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

        #region LotSupply

        public static LotSupplyDto ConvertToDto(this LotSupply lotSupply)
        {
            var supplyDto = new LotSupplyDto()
            {
                Id = lotSupply.Id,
                LotNumber = lotSupply.LotNumber,
                ExpDate = lotSupply.ExpDate,
                Default = lotSupply.Default,
                SupplyId = lotSupply.SupplyId,
                SupplyName = lotSupply.Supply.SupplyName
            };
            return supplyDto;
        }

        public static IEnumerable<LotSupplyDto> ConvertToDto(this IEnumerable<LotSupply> lotSupplies)
        {
            return lotSupplies.Select(ConvertToDto);
        }

        #endregion

        #region StrainType

        public static StrainTypeDto ConvertToDto(this StrainType strainType)
        {
            var strainTypeDto = new StrainTypeDto()
            {
                Id = strainType.Id,
                StrainId = strainType.StrainId,
                CategoryId = strainType.CategoryId,
                StrainName = strainType.Strain.StrainName,
                CategoryName = strainType.Category.CategoryName,
                InUse = strainType.InUse
            };
            return strainTypeDto;
        }


        public static IEnumerable<StrainTypeDto> ConvertToDto(this IEnumerable<StrainType> strainTypes)
        {
            return strainTypes.Select(ConvertToDto);
        }

        #endregion

        #region AppUser

        public static UserDto ConvertToDto(this AppUser user, IEnumerable<string> roles)
        {
            var userDto = new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = string.Join(", ", roles)
            };
            return userDto;
        }


        public static IEnumerable<UserDto> ConvertToDto(this IEnumerable<AppUser> users, IEnumerable<string> roles)
        {
            return users.Select(user=>user.ConvertToDto(roles));
        }

        #endregion

        #region SupplyProfile

        public static SupplyProfileDto ConvertToDto(this SupplyProfile supplyProfile)
        {
            var supplyProfileDto = new SupplyProfileDto()
            {
                Id = supplyProfile.Id,
                SupplyId = supplyProfile.SupplyId,
                QCProfileId = supplyProfile.QCProfileId,
                SortOrder = supplyProfile.SortOrder,
                SupplyName = supplyProfile.Supply.SupplyName,
                QCProfileName = supplyProfile.QCProfile.QCName,
                InUse = supplyProfile.InUse
            };
            return supplyProfileDto;
        }


        public static IEnumerable<SupplyProfileDto> ConvertToDto(this IEnumerable<SupplyProfile> supplyProfiles)
        {
            return supplyProfiles.Select(ConvertToDto);
        }

        #endregion

        #region QCProfile

        public static QCProfileDto ConvertToDto(this QCProfile qcProfile)
        {
            var qcProfileDto = new QCProfileDto()
            {
                Id = qcProfile.Id,
                LabId = qcProfile.LabId,
                MethodId = qcProfile.MethodId,
                CategoryId = qcProfile.CategoryId,
                QCName = qcProfile.QCName,
                Hide = qcProfile.Hide,
                LaboratoryName = qcProfile.Laboratory.LabName,
                CategoryName = qcProfile.Category.CategoryName,
                MethodName = qcProfile.Method.MethodName
            };
            return qcProfileDto;
        }


        public static IEnumerable<QCProfileDto> ConvertToDto(this IEnumerable<QCProfile> qcProfiles)
        {
            return qcProfiles.Select(ConvertToDto);
        }

        #endregion

        #region Category



        #endregion

        #region Category



        #endregion
    }
}
