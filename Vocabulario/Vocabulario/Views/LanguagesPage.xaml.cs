using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.Models;
using Vocabulario.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabulario.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LanguagesPage : CarouselPage
	{
		LanguagesViewModel viewModel;

		public LanguagesPage()
		{
			InitializeComponent();

			viewModel = new LanguagesViewModel();

			ItemsSource = viewModel.Languages;
		}

		async void AddLanguage_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewLanguagePage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Languages.Count == 0)
				viewModel.LoadLanguagesCommand.Execute(null);
		}

		async void OnLanguageButton_Clicked(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			Language language = (Language)btn.CommandParameter;
			await Navigation.PushAsync(new RanksPage(new RanksViewModel(language)));
		}
	}
}