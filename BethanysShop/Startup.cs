using BethanysShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanysShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            //by default an instance of appsettings.json gets passed in here
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services is a dependency injection container
            //register framework services here

            //services.AddTransient, add a new instance every time
            //services.AddSingleton, single instance for the entire application
            //service.AddScoped, singleton per request, discarded after each request, good to work incombination with data access

            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("AwsRdsConnection")));

            //added for identities functionality
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //scoped this all interaction in the same request is with the same shoppingcart
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            //used by shopping cart
            services.AddHttpContextAccessor();
            //also need middleware to support session
            services.AddSession();

            services.AddControllersWithViews();

            //added for identities functionality
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();

            //add middleware components here
            //handle http request and produce an http response
            //they are in a pipeline, the order they are added matters

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // forwarded headers for nginx
            var forwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwardedHeaderOptions.KnownNetworks.Clear();
            forwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardedHeaderOptions);

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //make sure to call UseSession before UseRouting, important in the middleware pipeline
            app.UseSession();

            app.UseRouting();

            //added for identities functionality
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //added for identities functionality
                endpoints.MapRazorPages();
            });
        }
    }
}
