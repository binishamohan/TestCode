using JuniorCodeTest.Interfaces;
using JuniorCodeTest.Models;

using System.Text.Json;

namespace JuniorCodeTest.Services
{
	public class RandomUserApiService(HttpClient httpClient) : IRandomUserApiService
	{
		private const string randomUserEndPoint = "https://randomuser.me/api/?results=5";

		public async Task<List<RequestedUsersModel>> GetRandomUserDataFromApi()
		{
			var requiredDataList = new List<RequestedUsersModel>();
			var response = await httpClient.GetAsync(randomUserEndPoint);

			if (response.IsSuccessStatusCode)
			{
				string responseData = await response.Content.ReadAsStringAsync();
				var randomUserModelData = JsonSerializer.Deserialize<UserModel>(responseData);

				if (randomUserModelData != null)
				{

					foreach (var randomUser in randomUserModelData.results)
					{
						var requiredData = new RequestedUsersModel()
						{
							Age = randomUser.dob.age,
							First = randomUser.name.first,
							Last = randomUser.name.last,
							Title = randomUser.name.title,
							Latitude = randomUser.location.coordinates?.latitude ?? "", // Adding null-conditional operator for Latitude
							Longitude = randomUser.location.coordinates?.longitude ?? "",// Adding null-conditional operator  for Longitude
							Country = randomUser.location?.country ?? ""// Adding null-conditional operator  for Country
						};

						if (requiredDataList.Count >= 5)
						{
							break;
						}

						requiredDataList.Add(requiredData);
					}

				}

				return requiredDataList;

			}
			else
			{
				throw new Exception("Failed to fetch data from the random user API");
			}
		}
	}
}
