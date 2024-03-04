using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;
using ServerLibrary.Configurations;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Phải nằm trên Fluent API
            // base.OnModelCreating(builder);

            #region Identity Config

            //builder.Entity<IdentityUser<Guid>>().ToTable("AppUsers");
            //builder.Entity<IdentityRole<Guid>>().ToTable("AppRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId });

            #endregion Identity Config

            #region Configure using Fluent API

            builder.ApplyConfiguration(new AppLogConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new LaboratoryConfiguration());
            builder.ApplyConfiguration(new LotSupplyConfiguration());
            builder.ApplyConfiguration(new LotTestConfiguration());
            builder.ApplyConfiguration(new MethodConfiguration());
            builder.ApplyConfiguration(new QCActionConfiguration());
            builder.ApplyConfiguration(new QCConfiguration());
            builder.ApplyConfiguration(new QCProfileConfiguration());
            builder.ApplyConfiguration(new QCProfileDetailConfiguration());
            builder.ApplyConfiguration(new ResultConfiguration());
            builder.ApplyConfiguration(new StandardConfiguration());
            builder.ApplyConfiguration(new StandardDetailConfiguration());
            builder.ApplyConfiguration(new StrainConfiguration());
            builder.ApplyConfiguration(new StrainGroupConfiguration());
            builder.ApplyConfiguration(new StrainTypeConfiguration());
            builder.ApplyConfiguration(new SupplyConfiguration());
            builder.ApplyConfiguration(new SupplyProfileConfiguration());
            builder.ApplyConfiguration(new TestProfileConfiguration());            
            builder.ApplyConfiguration(new TestQCConfiguration());
            builder.ApplyConfiguration(new TestTypeConfiguration());
            builder.ApplyConfiguration(new UseWithConfiguration());

            #endregion

            //Data seeding
            builder.Seed();
        }

        public DbSet<AppLog> AppLogs { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<LotSupply> LotSupplies { get; set; }
        public DbSet<LotTest> LotTests { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<QC> QCs { get; set; }
        public DbSet<QCAction> QCActions { get; set; }
        public DbSet<QCProfile> QCProfiles { get; set; }
        public DbSet<QCProfileDetail> QCProfileDetails { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<StandardDetail> StandardDetails { get; set; }
        public DbSet<Strain> Strains { get; set; }
        public DbSet<StrainGroup> StrainGroups { get; set; }
        public DbSet<StrainType> StrainTypes { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyProfile> SupplyProfiles { get; set; }
        public DbSet<TestProfile> TestProfiles { get; set; }
        public DbSet<TestQC> TestQCs { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<UseWith> UseWiths { get; set; }
    }
}
