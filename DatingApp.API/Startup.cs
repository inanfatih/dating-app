using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //bu method ile appsettings.json dosyasindaki configuration kullanilmis oluyor.
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Bu method dependency injection container olarak kullaniliyor
            // Uygulamanin herhangi bir yerinde kullanilmasini istedigimiz seyleri burada service olarak ekliyoruz
            // Buradaki siralama onemli degil.
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers().AddNewtonsoftJson(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors();
            // Asagidaki kod ile CloudinarySettings helper class indaki data ile appsettings teki datayi match ediyor
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper(typeof(DatingRepository).Assembly);

            // services.AddSingleton<IAuthRepository, AuthRepository>(); Bir service in instance ini bir kere olusturup onu tekrar tekrar kullaniyor. Bunu kullanmak async service ler icin (concurrent request icin) uygun degil.
            // services.AddTransient<IAuthRepository, AuthRepository>(); Lightweight stateless services icin kullaniliyor. Her request geldiginde tekrar tekrar kullaniliyor.
            services.AddScoped<IAuthRepository, AuthRepository>(); //Bu service scope dahilinde tekrar tekrar kullaniliyor. Ayni instance icinde tekrar tekrar kullaniliyor
            services.AddScoped<IDatingRepository, DatingRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Bir API a request yapilinca request, HTTP request pipeline dan geciyor. Configure method ile bu pipeline configure ediliyor.
            //Burasi bir middleware olarak calisiyor. Yani pipeline a gitmeden once buraya ugruyor
            //Configure method undaki configuration siralamasi onemli. 
            if (env.IsDevelopment())
            {
                //Asagidaki mesaj sayesinde development ta isek developer lar icin exception gosteriyor
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        // Asagidaki InternalServerError, Extensions class'ta tanimlandi ve HttpResponse'a hatalari ekliyor
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            //Burada HTTP request i HTTPS e direct ediyor
            // app.UseHttpsRedirection();

            app.UseRouting();
            // Asagida ".AllowCredentials" da olsaydi, o durumda cookies'den gelecek credentials i da kullaniyor olurdu. Fakat biz cookies ile islem yapmiyoruz sistemde
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}