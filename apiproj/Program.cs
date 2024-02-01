using apiproj.Models;
using apiproj.Services;
using apiproj.Repository;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFlight<HetalFlight>,FlightRepo>();
builder.Services.AddScoped<IFlightServ<HetalFlight>,FlightServ>();
 
builder.Services.AddDbContext<Ace52024Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
