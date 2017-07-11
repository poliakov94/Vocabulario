using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabulario.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OverviewPage : ContentPage
	{
		public OverviewPage()
		{
			InitializeComponent();
		}

		async void StartButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LanguagesPage());
		}
	}
}