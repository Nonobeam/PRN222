using Microsoft.AspNetCore.Authentication.Cookies;
using PsychologyHealthCare.Service;
using PsychologyHeathCare.RazorWebApp.Hubs;

namespace PsychologyHeathCare.RazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            builder.Services.AddScoped<IAppointmentTrackingService, AppointmentTrackingService>();
            builder.Services.AddScoped<ProgramTrackingService>();
            builder.Services.AddScoped<UserAccountService>();

            builder.Services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Forbidden");
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<PsychologyHealthCareHub>("/healthHub");

            app.MapRazorPages();

            app.Run();
        }
    }
}