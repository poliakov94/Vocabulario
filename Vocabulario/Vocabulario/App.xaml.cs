using Vocabulario.Services;
using Vocabulario.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Vocabulario
{
	public partial class App : Application
	{
		static LanguageDatabase database;

		public App()
		{
			InitializeComponent();
			SetMasterDetailPage();
		}

		public static LanguageDatabase Database
		{
			get
			{
				if(database == null)
				{
					database = new LanguageDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("LanguageSQLite.db3"));
				}
				return database;
			}
		}

		public static void SetMasterDetailPage()
		{
			Current.MainPage = new MasterDetailPageVocabulario();
		}

	}
}
