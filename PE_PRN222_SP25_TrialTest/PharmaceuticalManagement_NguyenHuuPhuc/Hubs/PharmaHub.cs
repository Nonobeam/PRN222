using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;

namespace PsychologyHeathCare.RazorWebApp.Hubs
{
	public class PharmaHub : Hub
	{
        private readonly IMedicineService _medicineService;
        private readonly ManufacturesService _manufacturesService;

        public PharmaHub(IMedicineService medicineService, ManufacturesService manufacturesService)
        {
            _medicineService = medicineService;
            _manufacturesService = manufacturesService;
        }

        public async Task CreateAppointmentTracking(string json)
		{
			try
			{
				var medicine = JsonConvert.DeserializeObject<MedicineInformation>(json);
				await _medicineService.Create(medicine!); // appointment! is not null reference

                medicine!.Manufacturer = await _manufacturesService.GetByIdAsync(medicine!.MedicineId);

				await Clients.All.SendAsync("Create_Appointment", medicine);

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
            MedicineInformation appointment = (await _medicineService.GetById(id))!;
			await _medicineService.Delete(appointment);
		}

		public async Task UpdateAppointment(string json)
		{
			var medicine = JsonConvert.DeserializeObject<MedicineInformation>(json);

			await _medicineService.Update(medicine!);
            medicine!.Manufacturer = await _manufacturesService.GetByIdAsync(medicine!.MedicineId);

            await Clients.All.SendAsync("Update_Appointment", medicine);
		}
	}
}