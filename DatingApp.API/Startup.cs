using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Bir API a request yapilinca request, HTTP request pipeline dan geciyor. Configure method ile bu pipeline configure ediliyor.
            //Burasi bir middleware olarak calisiyor. Yani pipeline a gitmeden once buraya ugruyor
            if (env.IsDevelopment())
            {
                //Asagidaki mesaj sayesinde development ta isek developer lar icin exception gosteriyor
                app.UseDeveloperExceptionPage();
            }

            //Burada HTTP request i HTTPS e direct ediyor
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}