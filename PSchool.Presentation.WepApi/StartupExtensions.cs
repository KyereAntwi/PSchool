using PSchool.Application;
using PSchool.Persistence;
using PSchool.Presentation.WepApi.Middleware;

namespace PSchool.Presentation.WepApi;

public static class StartupExtensions
{
    public static WebApplication ConfigurationServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddAplicationServices();
        builder.Services.AddPersistenceServices(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        builder.Services.AddControllers()
            .AddApplicationPart(typeof(Controllers.AssemblyRefrence).Assembly);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PSchool System");
            });
        }

        app.UseCors("Open");
        app.UseHttpsRedirection();

        //app.UseRouting();

        app.UseCustomExceptionHandler();

        
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
