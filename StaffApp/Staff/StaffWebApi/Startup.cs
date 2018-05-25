using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Staff.Infrastructure.Data;
using Staff.WebApi.Model;

namespace StaffWebApi
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
            services.AddDbContext<StaffContext>(c =>
            {
                try
                {
                    //c.UseInMemoryDatabase("Catalog");
                    c.UseSqlServer(Configuration.GetConnectionString("StaffDatabase"));
                    c.ConfigureWarnings(wb =>
                    {
                        //By default, in this application, we don't want to have client evaluations
                        wb.Log(RelationalEventId.QueryClientEvaluationWarning);
                    });
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            });

            services.AddMvc();

            services.AddCors(
                    options =>
                    options.AddPolicy("all", builder => builder
                                                .AllowAnyOrigin()
                                                .AllowAnyHeader()
                                                .AllowCredentials()
                                                .AllowAnyMethod()
                    )
                );

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin =
                        context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        };
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
                    options.Cookie.HttpOnly = false;
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                });

            services.AddTransient<StaffService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var ctx = serviceScope.ServiceProvider.GetService<StaffContext>();
                try
                {
                    var exists = ctx.Employees.Any();
                }
                catch (Exception e)
                {
                    ctx.Database.EnsureCreated();
                    ctx.EnsureSeedData();
                }
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var ctx = serviceScope.ServiceProvider.GetService<StaffContext>();
                try
                {
                    var exists = ctx.Employees.Any();
                }
                catch (Exception e)
                {
                    ctx.Database.EnsureCreated();
                    ctx.EnsureSeedData();
                }
            }

            app.UseAuthentication();

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //            Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
            //    RequestPath = "/Photos"
            //});

            app.UseCors("all");
            app.UseMvc();
        }
    }
}
