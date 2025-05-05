using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sales_Date_Prediction.API.Middleware;
using Sales_Date_Prediction.Application.Services;
using Sales_Date_Prediction.Domain.Entities;
using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.Domain.Context;
using Sales_Date_Prediction.Domain.Repositories;

namespace Sales_Date_Prediction.API
{
    public class Startup
    {
        private readonly string _title = "Sales Date Prediction";
        private readonly string _version = "v1";
        private readonly string _connectionString = "SqlServer";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevClient", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_version, new OpenApiInfo { Title = _title, Version = _version });
            });

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString(_connectionString),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 2,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                )
            ));

            services.AddScoped(typeof(IStoredProcedure<>), typeof(StoredProcedure<>));

            services.AddScoped<IReadService<Product>, ProductsService>();
            services.AddScoped<IReadService<Shipper>, ShippersService>();
            services.AddScoped<IReadService<Employee>, EmployeesService>();
            services.AddScoped<IReadService<Customer>, CustomerService>();
            services.AddScoped<IWriteService<Order>, OrdersService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseCors("AllowAngularDevClient");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_title} {_version}");
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
