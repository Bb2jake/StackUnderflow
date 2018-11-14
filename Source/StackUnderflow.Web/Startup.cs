using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackUnderflow.Business;
using StackUnderflow.Data;
using StackUnderflow.Entities;

namespace StackUnderflow.Web
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
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlite("Data Source=stackunderflow.db", b => b.MigrationsAssembly("StackUnderflow.Web")));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
				{
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			// Will return 401 instead of a redirect when a user attempts
			// to access an area without authorization.
			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToAccessDenied = context =>
				{
					context.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
			});
			services.AddTransient<UserManager<ApplicationUser>>();
			services.AddTransient<SignInManager<ApplicationUser>>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddCors();
			services.AddTransient<QuestionService>();
			services.AddTransient<CommentService>();
			services.AddTransient<AnswerService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
				builder.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
			);
			
			app.UseAuthentication();

			app.UseMvc();
		}
	}
}
