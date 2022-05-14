using  API.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>
    (options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")) );

var app = builder.Build();
 if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

  

app.UseHttpsRedirection();
 app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.Run();
