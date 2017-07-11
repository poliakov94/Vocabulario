using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabulario.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewLanguagePage : ContentPage
	{
		public Language Language { get; set; }
		public NewLanguagePage()
		{
			InitializeComponent();

			Language = new Language
			{
				Name = "...",
				Description = "...",
				Flag = "..."
			};

			BindingContext = this;
		}

		async void Save_Clicked(Object sender, EventArgs e)
		{
			Language.Flag = Language.Flag.ToLower() + ".png";
			MessagingCenter.Send(this, "AddLanguage", Language);
			await Navigation.PopToRootAsync();
		}
	}
}