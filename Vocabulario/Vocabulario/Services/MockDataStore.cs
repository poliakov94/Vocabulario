using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Vocabulario.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(Vocabulario.Services.MockDataStore))]
namespace Vocabulario.Services
{
	public class MockDataStore : IDataStore<Language>
	{
		bool isInitialized;
		List<Language> languages;
		
		public Task<bool> PullLatestAsync()
		{
			return Task.FromResult(true);
		}


		public Task<bool> SyncAsync()
		{
			return Task.FromResult(true);
		}

		public async Task<bool> AddLanguageAsync(Language language)
		{
			await InitializeAsync();
			await App.Database.SaveLanguageAsync(language);

			return await Task.FromResult(true);
		}

		public async Task<IEnumerable<Language>> GetLanguagesAsync(bool forceRefresh = false)
		{
			await InitializeAsync();
			languages = await App.Database.GetLanugagesAsync();

			return await Task.FromResult(languages);
		}

		public async Task<bool> DeleteLanguageAsync(Language language)
		{
			await InitializeAsync();

			var _language = languages.Where((Language arg) => arg.ID == language.ID).FirstOrDefault();
			languages.Remove(_language);
			await App.Database.DeleteLanguageAsync(language);

			return await Task.FromResult(true);
		}

		public async Task<Language> GetLanguageAsync(int id)
		{
			await InitializeAsync();

			return await App.Database.GetLanguageAsync(id);
		}

		public async Task<bool> UpdateLanguageAsync(Language language)
		{
			await InitializeAsync();
			await App.Database.SaveLanguageAsync(language);
			languages = await App.Database.GetLanugagesAsync();

			return await Task.FromResult(true);
		}

		public async Task InitializeMockAsync()
		{
			if (isInitialized)
				return;

			languages = new List<Language>();
			var _languages = new List<Language>
			{
				new Language { ID = 1, Name = "English", Description = "The cats are hungry", Flag = "united_kingdom.png"},
				new Language { ID = 2, Name = "Francais", Description = "Les chats ont faim", Flag = "france.png"},
				new Language { ID = 3, Name = "Espanol", Description = "Los gatos tienen hambre", Flag = "spain.png"},
				new Language { ID = 4, Name = "Italiano", Description = "I gatti hanno fame", Flag = "italy.png"},
				new Language { ID = 5, Name = "Deutsch", Description = "Die Katzen sind hungrig", Flag = "germany.png"},
				new Language { ID = 6, Name = "Polski", Description = "Koty są głodne", Flag = "poland.png"},
			};

			foreach (Language language in _languages)
			{
				languages.Add(language);
			}

			isInitialized = true;
		}


		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

			var _languages = new List<Language>()
			{
				new Language { Name = "English", Description = "The cats are hungry", Flag = "united_kingdom.png"},
				new Language { Name = "Francais", Description = "Les chats ont faim", Flag = "france.png"},
				new Language { Name = "Espanol", Description = "Los gatos tienen hambre", Flag = "spain.png"},
				new Language { Name = "Italiano", Description = "I gatti hanno fame", Flag = "italy.png"},
				new Language { Name = "Deutsch", Description = "Die Katzen sind hungrig", Flag = "germany.png"},
				new Language { Name = "Polski", Description = "Koty są głodne", Flag = "poland.png"},
			};

			foreach (Language language in _languages)
			{
				await App.Database.SaveLanguageAsync(language);
			}

			isInitialized = true;
		}
	}
}
