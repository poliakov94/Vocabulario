using Vocabulario.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Vocabulario
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			SetMasterDetailPage();
		}

		public static void SetMasterDetailPage()
		{
			Current.MainPage = new MasterDetailPageVocabulario();
		}

	}
}
