using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = ".NET Joke Service";
            });
            builder.Services.AddSingleton<AppointmentTrackingService>();
            builder.Services.AddHostedService<WindowsBackgroundService>();
            var host = builder.Build();
            host.Run();
        }
    }
}
