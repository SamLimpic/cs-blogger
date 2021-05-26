using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using cs_blogger.Repositories;
using cs_blogger.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace cs_blogger
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

            // TODO[epic=Auth] copy/paste
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsDevPolicy", builder =>
                {
                    builder
                      .WithOrigins(new string[]{
                          "http://localhost:8080",
                                "http://localhost:8081"
                                })
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                });
            });
            // end copy/paste


            services.AddControllers();

            // NOTE Transient Services
            services.AddScoped<AccountsService>();
            services.AddTransient<ProfilesService>();
            services.AddTransient<BlogsService>();
            services.AddTransient<CommentsService>();

            // NOTE Transient Repo's 
            services.AddScoped<AccountsRepository>();
            services.AddTransient<ProfilesRepository>();
            services.AddTransient<BlogsRepository>();
            services.AddTransient<CommentsRepository>();

            // TODO[epic=DB] database Connection
            services.AddScoped<IDbConnection>(x => CreateDbConnection());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "cs_blogger", Version = "v1" });
            });
        }

        // TODO[epic=DB] database Connection
        private IDbConnection CreateDbConnection()
        {
            string connectionString = Configuration["DB:gearhost"];
            return new MySqlConnection(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "cs_blogger v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // TODO[epic=Auth] Add Authenentication so bearer gets validated
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
