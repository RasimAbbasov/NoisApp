using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nois.API;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Application.Profiles;
using Nois.Application.Services;
using Nois.Application.Validators.CategoryValidators;
using Nois.Infrastructure.Services;
using Nois.Persistance.Contexts;
using Nois.Persistance.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Replace default .NET logging with Serilog
builder.Host.UseSerilog();

ServiceRegistration.RegisterServices(builder.Services, builder.Configuration);


// Add services to the container.

//builder.Services.AddControllers()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidator>());
//;
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<NoisDbContext>(options =>
//           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddAutoMapper(opt =>
//{
//    opt.AddProfile(new MapperProfile());
//});




//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IColorService, ColorService>();
//builder.Services.AddScoped<ISizeService, SizeService>();
//builder.Services.AddScoped<IProductService, ProductService>();




var app = builder.Build();

app.UseExceptionHandler();

app.UseMiddleware<BusinessExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Enables endpoint routing

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await RoleSeeder.SeedRolesAsync(services);
//}

app.Run();
