using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IServices;
using DataAccess;
using DataAccess.Repositories;
using Domain.IRepository;
using Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Security.IServices;
using Security.Repository;
using Security.Services;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" }); });
            
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
            
            services.AddDbContext<OrganizeeDbContext>(
                opt =>
                {
                    opt.UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=organizee.db");
                }, ServiceLifetime.Transient
            );
            
            services.AddCors(options =>
            {
                options.AddPolicy("Dev-cors", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                options.AddPolicy("Prod-cors", policy =>
                {
                    policy
                        .WithOrigins("")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
                app.UseCors("Dev-cors");
            }
            else
            {
                app.UseCors("Prod-cors");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}