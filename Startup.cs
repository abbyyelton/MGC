﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MGC.Models;
using AutoMapper;
using MGC.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MGC
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddDbContext<MyGiftClosetContext>();
            services.AddTransient<MyGiftClosetContextSeedData>();
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddMvc();
            services.AddIdentity<GiftUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";

            })
            .AddEntityFrameworkStores<MyGiftClosetContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MyGiftClosetContextSeedData seeder)
        {
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            Mapper.Initialize(config =>
            {
               
                config.CreateMap<GiftViewModel, Gift>()
                    .ForSourceMember(source => source.HolidayName, dest => dest.Ignore())
                    .ForSourceMember(source => source.RecipientName, dest => dest.Ignore());

                config.CreateMap<Gift, GiftViewModel>()
                   .ForMember(dest => dest.HolidayName, opt => opt.MapFrom(src => src.Holiday.Name))
                   .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.Recipient.Name));


            });

            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(config =>
           {
               config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" });
           });

            seeder.EnsureSeedData().Wait();
        }
    }
}
