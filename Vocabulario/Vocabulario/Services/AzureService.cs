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
	public class AzureService
	{
		public MobileServiceClient Client { get; set; }
		IMobileServiceSyncTable<Language> languageTable;

		public async Task Initialize()
		{
			if (Client?.SyncContext?.IsInitialized ?? false)
				return;

			var appUrl = "https://vocabulario.azurewebsites.net";


			Client = new MobileServiceClient(appUrl);

			var path = "syncstorevocabulario.db";
			path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);
			var store = new MobileServiceSQLiteStore(path);

			store.DefineTable<Language>();

			await Client.SyncContext.InitializeAsync(store);

			languageTable = Client.GetSyncTable<Language>();

			//await InitializeAsync();
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

		public async Task<ObservableCollection<Language>> GetLanguages()
		{
			await Initialize();
			await SyncLanguage();

			return await languageTable.ToCollectionAsync();
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

		public Language CreateRanks(Language language)
		{
			var ranks =  new List<Rank>()
			{
				new Rank { Name = "First", Description="Lorum Ipsum...", LanguageID = language.ID },
				new Rank { Name = "Second", Description="Lorum Ipsum...", LanguageID = language.ID },
				new Rank { Name = "Third", Description="Lorum Ipsum...", LanguageID = language.ID },
				new Rank { Name = "Fourth", Description="Lorum Ipsum...", LanguageID = language.ID }
			};

			language.Ranks = ranks;

			return language;
		}

		public async Task<Language> GetRanks(Language language)
		{
			await Initialize();
			return CreateRanks(language);
		}

	}
}
