using Connection;
using GraphCache;
using GraphService.NodoFather.Models;
using GraphService.NodoFather.Service;
using Microsoft.EntityFrameworkCore;
using MSSettings;
using Utils.Interfaces.Cache;

namespace GraphApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_GraphApi";
        private string strConnection = "";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            strConnection = Configuration.GetConnectionString("BD").ToString() ?? "";
            services.AddDbContext<ConnectionContext>(opt => opt.UseSqlServer(strConnection));
            services.AddSwagger("Graph Service v1", "Graph Service", "v1");
            services.AddCors(MyAllowSpecificOrigins);
            services.AddJson();
            services.AddHealthChecksUI();
            services.AddHealthChecksSqlServer(strConnection);
            services.AddHealthChecksContext<ConnectionContext>("ConnectionContext");

            //i18n
            services.AddI18N();

            //SERVICE
            services.AddScoped<INodoFatherService, NodoFatherService>();

            //CACHE
            services.AddScoped<ICache<NodoFatherDTO>, NodoFatherCache<NodoFatherDTO>>();
            services.AddMemoryCache();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            //app.UseCallLoggerMiddleware();
            //app.UseMiddleware<TraceMiddleware>();
            app.UseRouting();
            app.BuildSwagger("Graph Api v1");
            app.BuildCors(MyAllowSpecificOrigins);
            app.BuildHealthChecksUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
