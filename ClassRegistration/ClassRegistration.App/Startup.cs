using ClassRegistration.DataAccess.Entity;
using ClassRegistration.DataAccess.Interfaces;
using ClassRegistration.DataAccess.Repository;
using ClassRegistration.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ClassRegistration.App
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddControllers ().AddNewtonsoftJson (options =>
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<Course_registration_dbContext> (options =>
                 options.UseSqlServer (Configuration.GetConnectionString ("SqlServer")));

            services.AddScoped<IEnrollmentRepository, EnrollmentRepository> ();
            services.AddScoped<ICourseRepository, CourseRepository> ();
            services.AddScoped<IStudentRepository, StudentRepository> ();
            services.AddScoped<ISectionRepository, SectionRepository> ();
            services.AddScoped<IReviewsRepository, ReviewsRepository> ();
            services.AddScoped<IStudentTypeRepository, StudentTypeRepository> ();

            services.AddSwaggerGen ();
            /*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            })
            .AddOktaWebApi(new OktaWebApiOptions()
            {
                OktaDomain = "https://${yourOktaDomain}"
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c =>
             {
                 c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
                 c.RoutePrefix = string.Empty;
             });

            app.UseRouting ();

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints =>
            {
                endpoints.MapControllers ();
            });
        }
    }
}
