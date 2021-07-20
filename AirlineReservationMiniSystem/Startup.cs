using AirlineReservationMiniSystem.Hubs;
using AirlineReservationMiniSystem.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using AirlineReservationMiniSystem.Repositories;
using AirlineReservationMiniSystem.Repositories.Impl;

namespace AirlineReservationMiniSystem
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
			services.AddDbContext<AppDbContext>(options => 
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			// register framework services
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddRazorPages();
			// register our own services
			services.AddScoped<IFlightRepository, FlightRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IReservationRepository, ReservationRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();


			services.AddSignalR(c => c.EnableDetailedErrors = true);
			//services.AddTransient()
			//services.AddSingleton()

			services.AddAuthorization(options =>
		   {
			   options.AddPolicy("IsAdmin", policy =>
			   policy.RequireRole("Administrator"));
			   options.AddPolicy("IsAgent", policy =>
			   policy.RequireRole("Agent"));
			   options.AddPolicy("IsClient", policy =>
			   policy.RequireRole("Client"));
		   });
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
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			// app.UseSignalR(routes => routes.MapHub<ReservationHub>("/reservationhub"));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
				endpoints.MapHub<ReservationHub>("/reservationhub");
			});
		}
	}
}
