using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.ProductOptions.Commands;
using RefactorThisV2.Service.ProductOptions.Queries;
using RefactorThisV2.Service.Products.Commands;
using RefactorThisV2.Service.Products.Queries;

namespace RefactorThisV2
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

            services.AddMediatR(typeof(GetProductQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllProductsQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllProductsByNameQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateProductCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteProductCommandHandler).GetTypeInfo().Assembly);

            services.AddMediatR(typeof(GetProductOptionsQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetProductOptionQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateProductOptionCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateProductCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteProductOptionCommandHandler).GetTypeInfo().Assembly);

            services.AddDbContext<RefactorThisV2DbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
