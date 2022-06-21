using  API.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using API.Middleware;
using API.Helpers;
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddScoped<ITokenService , TokenService>();
builder.Services.AddScoped<IUserRepository , UserRepository>();
builder.Services.AddScoped<IPhotoService , PhotoService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();
  
 var scope = app.Services.CreateScope();
 var Services = scope.ServiceProvider;

var dataContext = Services.GetRequiredService<DataContext>();

 
try {
    await dataContext.Database.MigrateAsync();
    await Seed.SeedUsers(dataContext);
}

catch (Exception ex) {
 var logger = Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");

}


 if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

  
app.UseMiddleware<ExceptionMiddleware>();

 

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
