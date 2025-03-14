using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.WorkerService;

public sealed class WindowsBackgroundService(
    AppointmentTrackingService appointmentTrackingService,
    ILogger<WindowsBackgroundService> logger) : BackgroundService
{
    private string LogFilePath = @"serviceLog.txt";
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<AppointmentTracking> appoinments = await appointmentTrackingService.GetAllAsync();
				foreach (var appointment in appoinments)
				{
					LogToFile(appointment.ToString());
				}


				await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message}", ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
    }
    private void LogToFile(string message)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath, append: true))

                writer.WriteLine(message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to write to log file:{ErrorMessage}", ex.Message);
        }
    }
}