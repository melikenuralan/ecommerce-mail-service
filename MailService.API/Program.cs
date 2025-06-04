using MailService.API.Consumers;
using MailService.API.Extensions;
using MailService.API.Services;
using MailService.API.Services.Interfaces;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders(); 
    logging.AddConsole();    
 
});

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
