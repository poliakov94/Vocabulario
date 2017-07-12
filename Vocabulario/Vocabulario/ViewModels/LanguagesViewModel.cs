using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.Helpers;
using Vocabulario.Models;
using Vocabulario.Views;
using Xamarin.Forms;

namespace Vocabulario.ViewModels
{
	class LanguagesViewModel : BaseViewModel
	{
		public ObservableRangeCollection<Language> Languages { get; set; }
		public Command LoadLanguagesCommand { get; set; }

		public LanguagesViewModel()
		{
			Title = "Languages";
			Languages = new ObservableRangeCollection<Language>();
			LoadLanguagesCommand = new Command(async () => await ExecuteLoadLanguagesCommand());

			MessagingCenter.Subscribe<NewLanguagePage, Language>(this, "AddItem", async (obj, language) =>
			{
				var _language = language as Language;
				Languages.Add(_language);
				await DataStore.AddLanguageAsync(_language);
			});
		}

		async Task ExecuteLoadLanguagesCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				this.Languages.Clear();
				var languages = await DataStore.GetLanguagesAsync(true);
				//var languages = await App.Database.GetLanguagesAsync();
				this.Languages.ReplaceRange(languages);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load languages.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
