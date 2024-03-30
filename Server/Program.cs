using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Services;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "MQC Web API", 
        Version = "v1" 
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add application services.
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<IConfiguration>(config => builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddScoped<ILaboratoryService, LaboratoryService>();
builder.Services.AddScoped<IQCActionService, QCActionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMethodService, MethodService>();
builder.Services.AddScoped<IStandardService, StandardService>();
builder.Services.AddScoped<IStrainGroupService, StrainGroupService>();
builder.Services.AddScoped<IStrainService, StrainService>();
builder.Services.AddScoped<ITestTypeService, TestTypeService>();
builder.Services.AddScoped<ITestQCService, TestQCService>();
builder.Services.AddScoped<ISupplyService, SupplyService>();
builder.Services.AddScoped<ILotTestService, LotTestService>();
builder.Services.AddScoped<ILotSupplyService, LotSupplyService>();
builder.Services.AddScoped<IStrainTypeService, StrainTypeService>();
builder.Services.AddScoped<ISupplyProfileService, SupplyProfileService>();
builder.Services.AddScoped<IQCProfileService, QCProfileService>();
builder.Services.AddScoped<IStandardDetailService, StandardDetailService>();
builder.Services.AddScoped<IUseWithService, UseWithService>();
builder.Services.AddScoped<IQCService, QCService>();



builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowBlazorWasm", builder => builder
    .WithOrigins("https://localhost:7076;http://localhost:5002")
    .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

var jwtsection = builder.Configuration.GetSection("Jwt");

builder.Services.Configure<JwtSection>(jwtsection);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtsection.GetValue<string>("ValidIssuer"),
        ValidAudience = jwtsection.GetValue<string>("ValidAudience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsection.GetValue<string>("SecurityKey")!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Administrator"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
