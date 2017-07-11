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

			languages.Add(language);

			return await Task.FromResult(true);
		}

		public async Task<IEnumerable<Language>> GetLanguagesAsync(bool forceRefresh = false)
		{
			await InitializeAsync();

			return await Task.FromResult(languages);
		}
		public async Task<bool> AddLangugageAsync(Language language)
		{
			await InitializeAsync();

			languages.Add(language);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteLanguageAsync(Language language)
		{
			await InitializeAsync();

			var _language = languages.Where((Language arg) => arg.Id == language.Id).FirstOrDefault();
			languages.Remove(_language);

			return await Task.FromResult(true);
		}

		public async Task<Language> GetLanguageAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(languages.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Language>> GetLangugagesAsync(bool forceRefresh = false)
		{
			await InitializeAsync();

			return await Task.FromResult(languages);
		}

		public async Task<bool> UpdateLanguageAsync(Language language)
		{
			await InitializeAsync();

			var _language = languages.Where((Language arg) => arg.Id == language.Id).FirstOrDefault();
			languages.Remove(_language);
			languages.Add(language);

			return await Task.FromResult(true);
		}

		public async Task InitializeAsync()
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
	}
}
