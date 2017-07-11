using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabulario.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LanguageDetailPage : ContentPage
	{
		LanguageDetailViewModel languageDetailViewModel;

		public LanguageDetailPage()
		{
			InitializeComponent();
		}

		public LanguageDetailPage(LanguageDetailViewModel languageDetailViewModel)
		{
			InitializeComponent();
			BindingContext = this.languageDetailViewModel = languageDetailViewModel;
		}
	}
}