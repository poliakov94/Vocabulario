using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabulario.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailPageVocabularioMaster : ContentPage
	{
		public ListView ListView;

		public MasterDetailPageVocabularioMaster()
		{
			InitializeComponent();

			BindingContext = new MasterDetailPageVocabularioMasterViewModel();
			ListView = MenuItemsListView;
		}

		class MasterDetailPageVocabularioMasterViewModel : INotifyPropertyChanged
		{
			public ObservableCollection<MasterDetailPageVocabularioMenuItem> MenuItems { get; set; }

			public MasterDetailPageVocabularioMasterViewModel()
			{
				MenuItems = new ObservableCollection<MasterDetailPageVocabularioMenuItem>(new[]
				{
					new MasterDetailPageVocabularioMenuItem { Id = 0, Title = "Overview", TargetType = typeof(OverviewPage) },
					new MasterDetailPageVocabularioMenuItem { Id = 1, Title = "Languages", TargetType = typeof(LanguagesPage) },
					new MasterDetailPageVocabularioMenuItem { Id = 2, Title = "Progress", TargetType = typeof(ProgressPage) },
					new MasterDetailPageVocabularioMenuItem { Id = 3, Title = "Settings", TargetType = typeof(SettingsPage)},
					new MasterDetailPageVocabularioMenuItem { Id = 4, Title = "About", TargetType = typeof(AboutPage) },
				});
			}

			#region INotifyPropertyChanged Implementation
			public event PropertyChangedEventHandler PropertyChanged;
			void OnPropertyChanged([CallerMemberName] string propertyName = "")
			{
				if (PropertyChanged == null)
					return;

				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
			#endregion
		}
	}
}