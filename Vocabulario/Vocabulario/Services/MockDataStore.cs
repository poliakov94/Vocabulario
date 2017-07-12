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


		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

			

			var _languages = new List<Language>()
			{
				new Language { Name = "English", Description = "The cats are hungry", Flag = "united_kingdom.png", Ranks = CreateRanks()},
				new Language { Name = "Francais", Description = "Les chats ont faim", Flag = "france.png", Ranks = CreateRanks()},
				new Language { Name = "Espanol", Description = "Los gatos tienen hambre", Flag = "spain.png", Ranks = CreateRanks()},
				new Language { Name = "Italiano", Description = "I gatti hanno fame", Flag = "italy.png", Ranks = CreateRanks()},
				new Language { Name = "Deutsch", Description = "Die Katzen sind hungrig", Flag = "germany.png", Ranks = CreateRanks()},
				new Language { Name = "Polski", Description = "Koty są głodne", Flag = "poland.png", Ranks = CreateRanks()},
			};

			foreach (Language language in _languages)
			{
				await App.Database.SaveLanguageAsync(language);
			}

			isInitialized = true;
		}

		public List<Rank> CreateRanks()
		{
			return new List<Rank>()
			{
				new Rank { ID = 1, Name = "First", Words = new List<Word>() },
				new Rank { ID = 2, Name = "Second", Words = new List<Word>() },
				new Rank { ID = 3, Name = "Third", Words = new List<Word>() },
				new Rank { ID = 4, Name = "Fourth", Words = new List<Word>() }
			};
		}
	}
}
