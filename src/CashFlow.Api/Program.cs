using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "CashFlow API",
                Version = "v1",
            });
        });

        builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CashFlow API v1"));
        }

        app.UseMiddleware<CultureMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}