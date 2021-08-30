using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sales.Persistence.Context;
using Sales.Persistence.Repositories.Contracts.Models;
using Sales.Persistence.Repositories.Entities;
using Sales.Services;
using Sales.Services.Contracts;
using System;

namespace Sales.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // db context da aplicação
            services.AddDbContext<SalesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Sales.Persistence"))
            );

            // classes da camada repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<UnitOfWork>();

            // classes da camada de serviço
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddControllers();

            // configuração necessaria para mapear as classes de modelo com os seus dtos
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // config do swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Vendas Online",
                    Version = "1.0.0",
                    Description = "Api simples para registro de vendas. \n" +
                    "O objetivo desse projeto é exercitar desenvolvimento de Apis com ASP.NET Core Web Api",
                    Contact = new OpenApiContact()
                    {
                        Name = "Diogo Souza Santos",
                        Email = "ddiiogo.souza25710@hotmail.com",
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vendas online v1")
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
