using CsvHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.OpenApi.Models;
using Stereograph.TechnicalTest.Api.Constantes;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Repository;
using Stereograph.TechnicalTest.Api.Services;
using System;
using System.IO;

namespace Stereograph.TechnicalTest.Api;

public class Startup
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Startup(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string contentRootPath = _webHostEnvironment.ContentRootPath;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlite("Data Source=testtechnique.db")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        services.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stereograph.TechTechnique.Api", Version = "v1" });
            options.CustomSchemaIds(schema => schema.FullName);
        });

        services.AddTransient<CsvImporter>();
        services.AddTransient<IPersonRepository, PersonRepository>();

        services
            .AddControllers();
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment,IPersonRepository personRepository, CsvImporter csvImporter)
    {
        if (environment.IsDevelopment())
        {
            application
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stereograph.TechTechnique.Api V1"));
        }

        application
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors()
            .UseEndpoints(endpoints => endpoints.MapControllers());


        using IServiceScope scope = application.ApplicationServices.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        ApplicationDbContext appDbContext = services.GetRequiredService<ApplicationDbContext>();
        appDbContext.Database.EnsureCreated();
        if(personRepository.IsEmptyTable())
            csvImporter.Import();
    }
}
