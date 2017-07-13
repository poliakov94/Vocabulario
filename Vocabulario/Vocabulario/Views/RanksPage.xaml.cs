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
	public partial class RanksPage : ContentPage
	{
		RanksViewModel viewModel;

		public RanksPage(RanksViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Rank;
			if (item == null)
				return;

			//await Navigation.PushAsync(new WordsPage(new WordsViewModel(item)));

			// Manually deselect item
			ItemsListView.SelectedItem = null;
		}

		async void AddItem_Clicked(object sender, EventArgs e)
		{
			//await Navigation.PushAsync(new NewRankPage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Ranks.Count == 0)
				viewModel.LoadRanksCommand.Execute(null);
		}
	}
}