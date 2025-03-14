using Microsoft.AspNetCore.SignalR;

namespace PsychologyHeathCare.RazorWebApp.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("Receive_Message", user, message);
		}
	}
}
