using PSchool.Presentation.WepApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


var app = builder.ConfigurationServices().ConfigurePipeline();

app.Run();