using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EgitimModuluApp.Services;
using EgitimModuluApp.Filter;
using EgitimModuluApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using EgitimModuluApp.DataAccessLayer;
using EgitimModuluApp.Services.AccountService;

namespace EgitimModuluApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var a = Configuration.GetValue<int>("f");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Tüm Controller'lar ValidationFilter ile kontrol edilir. 
            services.AddControllers(o =>
            {
                o.Filters.Add(new ValidationFilter());
            });

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Frontend/build";
            });
            services.AddAutoMapper(typeof(Startup));

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<IAuditHelper, AuditHelper>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDataAccessContext, DataAccessContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Cors ayarı yapıldı(Allow All)
                app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                context.Database.Migrate();
                DataSeeder.Seed(context);
            }

            // Genel hata yönetimi. Geriye 500 döner.
            app.UseCustomException();

            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            // if (Configuration.GetValue<int>("f") == 0)
            // {
            //     app.UseSpa(spa =>
            //                 {
            //                     spa.Options.SourcePath = "Frontend";

            //                     if (env.IsDevelopment())
            //                     {
            //                         spa.UseReactDevelopmentServer(npmScript: "start");
            //                     }
            //                 });
            // }
        }
    }

}
