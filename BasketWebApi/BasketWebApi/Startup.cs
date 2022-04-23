using BasketModuleDal;
using BasketModuleDal.Context;
using BasketWebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace BasketWebApi
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
            services.AddDbContext<BasketModuleContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.BuildServiceProvider().GetService<BasketModuleContext>().Database.Migrate();//Otomatik kurulum istediğiniz için migrate komutunu aktif ettim.

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();

            //Not: Swagger için authenticate işlemleri eklemedim. Bir user token alamadan veya swaggera yetkisi olmayan bir kullanıcı normalde girmemeli.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketWebApi", Version = "v1" });
            });

            services.AddLogging();

            //Not:Bu case icin sadece text dosya üzerine log alıyorum ancak normalde elasticsearch, seq, db gibi projenin gerektirdiği log toolları üzerine log alıp analiz raporları çıkarılabilir.
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", Configuration["ApplicationName"])
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .MinimumLevel.Override("System", LogEventLevel.Debug)
                .WriteTo.File("logs/" + Configuration["ApplicationName"] + "-log.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Verbose()
                .CreateLogger();
            Log.Information("Log Init Complete");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<SerilogMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
