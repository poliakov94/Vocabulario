using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.Models;
using Vocabulario.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace Vocabulario.Services
{
	public class AzureService //TODO: Create interface IAzureService
	{
		public MobileServiceClient Client { get; set; }
		IMobileServiceSyncTable<Language> languageTable;
		IMobileServiceSyncTable<Rank> rankTable;
		IMobileServiceSyncTable<Word> wordTable;

		public async Task Initialize()
		{
			if (Client?.SyncContext?.IsInitialized ?? false)
				return;

			var appUrl = "https://vocabulario.azurewebsites.net";


			Client = new MobileServiceClient(appUrl);

			var path = "syncstorevocabulariotest.db";
			path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);
			var store = new MobileServiceSQLiteStore(path);

			store.DefineTable<Language>();
			store.DefineTable<Rank>();
			store.DefineTable<Word>();

			await Client.SyncContext.InitializeAsync(store);

			languageTable = Client.GetSyncTable<Language>();
			rankTable = Client.GetSyncTable<Rank>();
			wordTable = Client.GetSyncTable<Word>();

			//await InitializeAsync();
			//await CreateRanks();
		}

		public async Task SyncLanguage()
		{
			try
			{
				if (!CrossConnectivity.Current.IsConnected)
					return;

				await languageTable.PullAsync("allLanguage", languageTable.CreateQuery());

				await Client.SyncContext.PushAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to sync languages, going offline: " + ex);
			}
		}

		public async Task SyncRank()
		{
			try
			{
				if (!CrossConnectivity.Current.IsConnected)
					return;

				await rankTable.PullAsync("allRank", rankTable.CreateQuery());

				await Client.SyncContext.PushAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to sync ranks, going offline: " + ex);
			}
		}

		public async Task SyncWord()
		{
			try
			{
				if (!CrossConnectivity.Current.IsConnected)
					return;

				await wordTable.PullAsync("allWord", wordTable.CreateQuery());

				await Client.SyncContext.PushAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to sync words, going offline: " + ex);
			}
		}

		public async Task<ObservableCollection<Language>> GetLanguages()
		{
			await Initialize();
			await SyncLanguage();

			return await languageTable.ToCollectionAsync();
		}

		public async Task<ObservableCollection<Rank>> GetRanks()
		{
			await Initialize();
			await SyncRank();

			return await rankTable.ToCollectionAsync();
		}

		public async Task<ObservableCollection<Rank>> GetRanks(string languageID)
		{
			await Initialize();
			await SyncRank();

			return await rankTable.Where(i => i.LanguageID == languageID).ToCollectionAsync();
		}

		public async Task<ObservableCollection<Word>> GetWords()
		{
			await Initialize();
			await SyncWord();

			return await wordTable.ToCollectionAsync();
		}

		public async Task<ObservableCollection<Word>> GetWords(string rankID)
		{
			await Initialize();
			await SyncWord();

			return await wordTable.Where(i => i.RankID == rankID).ToCollectionAsync();
		}

		public async Task<Language> SaveLanguage(Language language)
		{
			await Initialize();

			if (language.ID != null)
				await languageTable.UpdateAsync(language);
			else
				await languageTable.InsertAsync(language);

			await SyncLanguage();

			return language;
		}

		public async Task<Rank> SaveRank(Rank rank)
		{
			await Initialize();

			if (rank.ID != null)
				await rankTable.UpdateAsync(rank);
			else
				await rankTable.InsertAsync(rank);

			await SyncLanguage();

			return rank;
		}

		public async Task<Word> SaveWord(Word word)
		{
			await Initialize();

			if (word.ID != null)
				await wordTable.UpdateAsync(word);
			else
				await wordTable.InsertAsync(word);

			await SyncLanguage();

			return word;
		}

		public async Task InitializeAsync()
		{			
			var _languages = new List<Language>()
			{
				new Language { Name = "English" , Description = "The cats are hungry", Flag = "united_kingdom.png"},
				new Language { Name = "Francais", Description = "Les chats ont faim", Flag = "france.png"},
				new Language { Name = "Espanol", Description = "Los gatos tienen hambre", Flag = "spain.png"},
				new Language { Name = "Italiano", Description = "I gatti hanno fame", Flag = "italy.png"},
				new Language { Name = "Deutsch", Description = "Die Katzen sind hungrig", Flag = "germany.png"},
				new Language { Name = "Polski", Description = "Koty są głodne", Flag = "poland.png"},
			};

			foreach (Language language in _languages)
			{
				await SaveLanguage(language);
			}			
		}

		public async Task CreateRanks()
		{
			var _languages = await GetLanguages();
			foreach (Language language in _languages)
			{
				var _ranks = new List<Rank>()
				{
					new Rank { Name = "First", Description="Lorum Ipsum...", LanguageID = language.ID },
					new Rank { Name = "Second", Description="Lorum Ipsum...", LanguageID = language.ID },
					new Rank { Name = "Third", Description="Lorum Ipsum...", LanguageID = language.ID },
					new Rank { Name = "Fourth", Description="Lorum Ipsum...", LanguageID = language.ID }
				};

				foreach (Rank rank in _ranks)
				{
					await SaveRank(rank);
				}
			}
		}
	}
}
