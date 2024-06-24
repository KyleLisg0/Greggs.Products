using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Extensions;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Greggs.Products.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen();

        services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(GetAllProductsHandler).Assembly));

        services.AddSingleton<IDataAccess<Product>, ProductAccess>();

        var settings = new Settings();
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
        builder.Build()
            .Bind(settings);

        services.AddSingleton<IProductConverter, ProductConverter>(c =>
            new ProductConverter(settings.CurrencyConversionRates));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greggs Products API V1"); });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}