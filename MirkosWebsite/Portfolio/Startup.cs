using Covid.Core.Interfaces;
using Covid.Core.SqlImplementations;
using Food.Core.Interfaces;
using Food.Core.SqlImplementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicTechnologies.Core.Interfaces;
using MusicTechnologies.Core.SqlImplementation;
using MusicTechnologies.Data;

namespace Portfolio
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
            services.AddDbContextPool<QuaverflowDbContext>
            (options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("QuaverflowDb"));
            });
            services.AddScoped<IIntervalData, SqlInterval>();
            services.AddScoped<IModeData, SqlMode>();
            services.AddScoped<IScaleData, SqlScale>();
            services.AddScoped<IScaleCalculator, SqlScaleCalculator>();

            services.AddScoped<IIngredientData, SqlIngredient>();
            services.AddScoped<IIngredientGroupData, SqlIngredientGroup>();
            services.AddScoped<IIngredientSubGroupData, SqlIngredientSubGroup>();
            services.AddScoped<IRecipeIngredientData, SqlRecipeIngredient>();
            services.AddScoped<IRecipeData, SqlRecipe>();

            services.AddScoped<ICountryData, SqlCountry>();
            services.AddSession();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
