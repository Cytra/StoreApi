using Application;
using Application.Factories;
using Application.Models;
using Application.Models.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StoreApi.Middleware;
using Swashbuckle.AspNetCore.Filters;

namespace StoreApi;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddHttpClients();
        services.AddInfrastructureServices<Startup>(_configuration);
        services.Configure<AppOptions>(_configuration);


        services.AddControllers()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var apiResponse = new ErrorResponse();
                    foreach (var modelState in context.ModelState)
                    foreach (var error in modelState.Value.Errors)
                        apiResponse.Errors.Add(ErrorFactory.GetError(ErrorCode.ValidationError, error.ErrorMessage));
                    return new BadRequestObjectResult(apiResponse);
                };
            });
        services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store API", Version = "v1" });
            c.CustomSchemaIds(x => x.FullName?.Replace("+", "."));
            c.ExampleFilters();
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreApi v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}