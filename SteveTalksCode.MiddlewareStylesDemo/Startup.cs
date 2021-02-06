using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SteveTalksCode.MiddlewareStylesDemo
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SteveTalksCode.MiddlewareStylesDemo", Version = "v1" });
            });
            services.AddScoped<GuidInstance>();

            // register our factory middlewares, but not convention middlewares as created with reflection
            
            services.AddSingleton<SingletonFactoryGuidMiddlewareByConstructor>();
            services.AddSingleton<SingletonFactoryGuidMiddlewareByInvokeMethod>();
            
            services.AddScoped<ScopedFactoryGuidMiddlewareByConstructor>();
            services.AddScoped<ScopedFactoryGuidMiddlewareByInvokeMethod>();

            services.AddTransient<TransientFactoryGuidMiddlewareByConstructor>();
            services.AddTransient<TransientFactoryGuidMiddlewareByInvokeMethod>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SteveTalksCode.MiddlewareStylesDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var guidInstance = context.RequestServices.GetRequiredService<GuidInstance>();
                context.Items.Add($"{GuidInstance.ContextKey}_{nameof(Startup)}", $"By direct in-line function : {guidInstance} - scoped as resolved from context.RequestServices (which is a scoped container");
                await next();
            });

            // register convention middleware that will be instantiated by reflection
            app.UseMiddleware<ConventionalGuidMiddlewareByConstructor>();
            app.UseMiddleware<ConventionalGuidMiddlewareByInvokeMethod>();

            // register factory middlewares that will be instantiated by reflection
            app.UseMiddleware<SingletonFactoryGuidMiddlewareByConstructor>();
            app.UseMiddleware<SingletonFactoryGuidMiddlewareByInvokeMethod>();
            app.UseMiddleware<ScopedFactoryGuidMiddlewareByConstructor>();
            app.UseMiddleware<ScopedFactoryGuidMiddlewareByInvokeMethod>();
            app.UseMiddleware<TransientFactoryGuidMiddlewareByConstructor>();
            app.UseMiddleware<TransientFactoryGuidMiddlewareByInvokeMethod>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
