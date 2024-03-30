using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;

namespace ServerLibrary.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var lab = new Laboratory()
            {
                Id = 1,
                OrganizationName = "BVĐKTƯ Cần Thơ",
                LabName = "Khoa Xét Nghiệm",
                Address = "315 Nguyễn Văn Linh - An Khánh - Ninh Kiều - Tp. Cần Thơ",
                LabStatus = LabStatus.Active
            };
            modelBuilder.Entity<Laboratory>().HasData(lab);

            Guid userId = Guid.NewGuid();          

            var newUser = new AppUser()
            {
                Id = userId,
                LabId = 0,
                FirstName = "Hoằng",
                LastName = "Nguyễn Tấn",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "123456789",
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN",
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("@dmin123"),
                //Laboratory = lab
            };
            modelBuilder.Entity<AppUser>().HasData(newUser);
            
            Guid roleId = Guid.NewGuid();

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = roleId,
                    Name = "Administrator",
                    Description = "Quản trị hệ thống"
                },
                new AppRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manager",
                    Description = "Quản lý"
                },
                new AppRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    Description = "Người dùng"
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>() { UserId = userId, RoleId = roleId });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    CategoryName = "Định danh",
                },
                new Category()
                {
                    Id = 2,
                    CategoryName = "Thử nghiệm",
                },
                new Category()
                {
                    Id = 3,
                    CategoryName = "Kháng sinh đồ",
                });

            modelBuilder.Entity<StrainGroup>().HasData(
                new StrainGroup()
                {
                    Id = 1,
                    GroupName = "Gram âm",
                },
                new StrainGroup()
                {
                    Id = 2,
                    GroupName = "Gram dương",
                },
                new StrainGroup()
                {
                    Id = 3,
                    GroupName = "Vi nấm",
                });

            modelBuilder.Entity<Method>().HasData(
                new Method()
                {
                    Id = 1,
                    MethodName = "Thủ công",
                },
                new Method()
                {
                    Id = 2,
                    MethodName = "Tự động trên máy Vitek",
                },
                new Method()
                {
                    Id = 3,
                    MethodName = "Tự động trên máy BD",
                });

            modelBuilder.Entity<TestType>().HasData(
                new TestType()
                {
                    Id = 1,
                    TypeName = "Kháng sinh đĩa giấy",
                    Unit = "mm"
                },
                new TestType()
                {
                    Id = 2,
                    TypeName = "Kháng sinh E-test"
                },
                new TestType()
                {
                    Id = 3,
                    TypeName = "Kháng sinh tự động",
                    Unit = "µg/ml"
                },
                new TestType()
                {
                    Id = 4,
                    TypeName = "Đĩa thử nghiệm",
                    Unit = "mm"
                },
                new TestType()
                {
                    Id = 5,
                    TypeName = "Định danh tự động"
                });
        }

        //private static void SeedLaboratories(ModelBuilder modelBuilder)
        //{
        //    var newlab = new Laboratory()
        //    {
        //        Id = 1,
        //        OrganizationName = "BVĐKTƯ Cần Thơ",
        //        LabName = "Khoa Xét Nghiệm",
        //        Address = "315 Nguyễn Văn Linh - An Khánh - Ninh Kiều - Tp. Cần Thơ",
        //        LabStatus = LabStatus.Active,
        //        Users = new List<AppUser> { }
        //    };

        //    modelBuilder.Entity<Laboratory>().HasData(newlab);
        //}
    }
}