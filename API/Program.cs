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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITokenService , TokenService>();

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();


 if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

  
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
