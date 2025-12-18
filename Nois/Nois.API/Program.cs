using Nois.API;
using Nois.Application.Exceptions;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await RoleSeeder.SeedRolesAsync(services);
//}

app.Run();
