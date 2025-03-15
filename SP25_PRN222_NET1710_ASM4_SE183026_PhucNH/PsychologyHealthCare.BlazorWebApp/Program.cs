using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server;
using PsychologyHealthCare.BlazorWebApp.Components;
using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.BlazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<CircuitOptions>(options => options.DetailedErrors = true);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "auth_token";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/access-denied";
                    options.Cookie.MaxAge = TimeSpan.FromDays(30);
                });

            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddScoped<UserAccountService>();
            builder.Services.AddScoped<IAppointmentTrackingService, AppointmentTrackingService>();
            builder.Services.AddScoped<ProgramTrackingService>();

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
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}