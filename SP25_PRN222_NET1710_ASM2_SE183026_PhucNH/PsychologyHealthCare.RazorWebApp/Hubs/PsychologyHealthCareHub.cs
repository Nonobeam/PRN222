using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;

namespace PsychologyHeathCare.RazorWebApp.Hubs
{
	public class PsychologyHealthCareHub : Hub
	{
        private readonly IAppointmentTrackingService _appointmentService;
        private readonly ProgramTrackingService _programService;

        public PsychologyHealthCareHub(IAppointmentTrackingService appointmentService, ProgramTrackingService programService)
        {
            _appointmentService = appointmentService;
            _programService = programService;
        }

        public async Task CreateAppointmentTracking(string json)
		{
			try
			{
				var appointment = JsonConvert.DeserializeObject<AppointmentTracking>(json);
				await _appointmentService.Create(appointment!); // appointment! is not null reference

                appointment!.ProgramTracking = await _programService.GetById(appointment!.Id);

				await Clients.All.SendAsync("Create_Appointment", appointment);

				#region Call domain application or api service here

				#endregion
			}
			catch (Exception)
			{
			}
		}

		public async Task DeleteAppointment(string id)
		{
			await Clients.All.SendAsync("Delete_Appointment", id);
			AppointmentTracking appointment = (await _appointmentService.GetById(id))!;
			await _appointmentService.Delete(appointment);
		}

		public async Task UpdateAppointment(string json)
		{
			var appointment = JsonConvert.DeserializeObject<AppointmentTracking>(json);

			await _appointmentService.Update(appointment!);

			appointment!.ProgramTracking = await _programService.GetById(appointment.ProgramTrackingId);

			await Clients.All.SendAsync("Update_Appointment", appointment);
		}
	}
}