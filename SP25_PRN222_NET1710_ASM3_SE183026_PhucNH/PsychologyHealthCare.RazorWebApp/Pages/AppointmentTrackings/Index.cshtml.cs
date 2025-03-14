using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsychologyHealthCare.Service;
using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string Name { get; set; }

        public string Rating { get; set; }

        public string Address { get; set; }

        public int PageNumber { get; set; }

        public readonly int PageSize = 3;

        public int TotalPages { get; set; }

        public IList<AppointmentTracking> AppointmentTracking { get; set; } = default!;

        private readonly IAppointmentTrackingService _appointmentService;

        public IndexModel(IAppointmentTrackingService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        public async Task OnGetAsync(string name, string rating, string address, int pageNumber = 1)
        {
            var appointments = string.IsNullOrEmpty(name) && string.IsNullOrEmpty(rating) && string.IsNullOrEmpty(address)
                ? await _appointmentService.GetAllAsync()
                : await _appointmentService.Search(name, rating, address);

            var totalItems = appointments.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double) PageSize);

            var pagedAppointments = appointments
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            Initialize(name, rating, address, pageNumber, totalPages, pagedAppointments);
        }

        private void Initialize(string name, string rating, string address, int pageNumber, int totalPages, IList<AppointmentTracking> appointmentTrackings)
        {
            Name = name;
            Rating = rating;
            Address = address;
            PageNumber = pageNumber;
            TotalPages = totalPages;
            AppointmentTracking = appointmentTrackings;
        }
    }
}
