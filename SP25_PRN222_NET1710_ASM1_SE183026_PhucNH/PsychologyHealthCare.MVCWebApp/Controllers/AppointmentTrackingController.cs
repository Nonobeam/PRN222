using Microsoft.AspNetCore.Mvc;
using PsychologyHealthCare.Service;
using PsychologyHealthCare.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychologyHealthCare.MVCWebApp.Models;

namespace PsychologyHealthCare.MVCWebApp.Controllers
{
    public class AppointmentTrackingController : Controller
    {
        private readonly IAppointmentTrackingService _appointmentService;
        private readonly ProgramTrackingService _programService;
        public readonly int PageSize = 3;

        public AppointmentTrackingController(IAppointmentTrackingService appointmentService, ProgramTrackingService programService)
        {
            _appointmentService = appointmentService;
            _programService = programService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string name, string rating, string address, int pageNumber = 1)
        {
            ViewData["name"] = name;
            ViewData["rating"] = rating;
            ViewData["address"] = address;

            var appointments = string.IsNullOrEmpty(name) && string.IsNullOrEmpty(rating) && string.IsNullOrEmpty(address)
                ? await _appointmentService.GetAllAsync()
                : await _appointmentService.Search(name, rating, address);

            var appointmentsViewModel = GetPagedAppointments(appointments, pageNumber, PageSize);

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(rating) || !string.IsNullOrEmpty(address))
            {
                appointmentsViewModel.Name = name;
                appointmentsViewModel.Rating = rating;
                appointmentsViewModel.Address = address;
            }

            return View(appointmentsViewModel);
        }

        private AppointmentViewModel GetPagedAppointments(IEnumerable<AppointmentTracking> appointments, int pageNumber, int pageSize)
        {
            var totalItems = appointments.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedAppointments = appointments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new AppointmentViewModel
            {
                Appointments = pagedAppointments,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            var appointment = await _appointmentService.GetById(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["ProgramId"] = new SelectList(await _programService.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentTracking appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentService.Create(appointment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramId"] = new SelectList(await _programService.GetAllAsync(), "Id", "Name");
            return View(appointment);
        }

        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var appointment = await _appointmentService.GetById(id);
            if (appointment == null) return NotFound();
            ViewData["ProgramId"] = new SelectList(await _programService.GetAllAsync(), "Id", "Name");
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Edit([Bind("Id,Name,StartTime,EndTime,Rating,Holder,Address,ProgramTrackingId,CreatedDate,UpdatedDate,Type,SystemStatus")] AppointmentTracking appointmentTracking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _appointmentService.Update(appointmentTracking);
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction(nameof(Index));
            }

            return View(appointmentTracking);
        }

        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var appointment = await _appointmentService.GetById(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appointment = await _appointmentService.GetById(id);
            if (appointment != null)
            {
                await _appointmentService.Delete(appointment);

            }

            return RedirectToAction(nameof(Index));
        }

        private byte[] TimeSpanToByteArray(TimeSpan time)
        {
            long ticks = time.Ticks; // Lấy số ticks (đơn vị 100 nanoseconds)
            return BitConverter.GetBytes(ticks); // Chuyển thành byte[]
        }

        private bool TestExists(String id)
        {
            return _appointmentService.GetAllAsync().Result.Any(s => s.Id == id);
        }
    }
}
