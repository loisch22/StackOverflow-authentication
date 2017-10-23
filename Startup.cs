using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Models;
//gives Startup class access to Identity
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StackOverflow
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; set; }
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json");
			Configuration = builder.Build();
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddEntityFrameworkMySql()
					.AddDbContext<ApplicationDbContext>(options =>
											  options
												   .UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));
			//tells Identity what we want to use as a model for our user
			services.AddIdentity<ApplicationUser, IdentityRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//has to be before app.UseMvc
			app.UseIdentity();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					//when project loaded, lead to Account/Index view
					template: "{controller=Account}/{action=Index}/{id?}");
			});


			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("This page is under construction");
			});
		}
	}
}