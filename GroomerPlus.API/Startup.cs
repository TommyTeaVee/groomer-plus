﻿using GroomerPlus.Core.Repositories;
using GroomerPlus.Infrastructure.Repositories.InMemory;
using GroomerPlus.Infrastructure.Repositories.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroomerPlus.API
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
            services.AddMvc();
            services.AddMediatR(typeof(Startup));

            var connection = @"Server=(localdb)\mssqllocaldb;Database=GroomerPlus;Trusted_Connection=True;";
            services.AddDbContext<GroomingContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IClientRepository, SqlServerClientRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
