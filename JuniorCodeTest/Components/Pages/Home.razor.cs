using JuniorCodeTest.Interfaces;
using JuniorCodeTest.Models;

using Microsoft.AspNetCore.Components;


namespace JuniorCodeTest.Components.Pages
{
	public partial class Home : ComponentBase
	{
		public List<RequestedUsersModel> RandomUsers { get; set; } = [];

		[Inject]
		private IRandomUserApiService? RandomUserApiService { get; set; }


		

		protected override async Task OnInitializedAsync()
		{
			await RefreshUsers();
		}

		private async Task RefreshUsers()
		{
			try
			{
				RandomUsers = await RandomUserApiService.GetRandomUserDataFromApi();
				RandomUsers = RandomUsers.Take(5).ToList(); // Take only the first 5 random users
			}
			catch (Exception ex)
			{
				// Handle exception
				Console.WriteLine(ex.Message);
			}
		}
	}
}
