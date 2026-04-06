using Nois.API;
using Nois.Application.Exceptions;
using Nois.Infrastructure.Services;
using Nois.Persistance.Contexts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Replace default .NET logging with Serilog
builder.Host.UseSerilog();


ServiceRegistration.RegisterServices(builder.Services, builder.Configuration);

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

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await RoleSeeder.SeedRolesAsync(services);
//}

//During Integration Testing: If you have hundreds of tests that spin up the app, adding 4 seconds to every startup will make your tests slow.

//Temporary solve of reducing first run time 
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NoisDbContext>();
    // This forces EF Core to build the entire model and cache it
    _ = context.Model;
}

app.Run();
