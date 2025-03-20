using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsychologyHealthCare.Service;
using PsychologyHealthCare.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize(Roles = "2, 3")]
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Rating { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Address { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

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

            TotalPages = totalPages;
            AppointmentTracking = pagedAppointments;
        }
    }
}
