namespace DiffProject
{
    using DiffProject.Infrastructure.V1;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });
			services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyInputFormatter()));
            services.AddApiVersioning(o => o.ReportApiVersions = true);

            services.AddTransient<IHashStrategy, Md5HashStrategy>();
            services.AddTransient<ICache, DotNetMemoryCache>();
            services.AddTransient<IEncodeDecodeStrategy, Base64EncodeDecodeStrategy>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
