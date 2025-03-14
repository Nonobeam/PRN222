using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;

namespace PharmaceuticalManagement_NguyenHuuPhuc.Hubs
{
	public class PharmaHub : Hub
	{
        private readonly IMedicineService _medicineService;

        public PharmaHub(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

		public async Task DeleteAppointment(string id)
		{
			await Clients.All.SendAsync("Delete_Medicine", id);
            MedicineInformation appointment = (await _medicineService.GetById(id))!;
			await _medicineService.Delete(appointment);
		}
	}
}