using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.Helpers;
using Vocabulario.Models;
using Xamarin.Forms;

namespace Vocabulario.ViewModels
{
	public class RanksViewModel : BaseViewModel
	{
		public Language _Language { get; set; }
		public ObservableRangeCollection<Rank> Ranks { get; set; }
		public Command LoadRanksCommand { get; set; }

		public RanksViewModel(Language _language = null)
		{
			_Language = _language;
			Title = _Language.Name;
			Ranks = new ObservableRangeCollection<Rank>();
			LoadRanksCommand = new Command(async () => await ExecuteLoadRanksCommand());

		}

		async Task ExecuteLoadRanksCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Ranks.Clear();
				var ranks = await Service.GetRanks(_Language);
				Ranks.ReplaceRange(ranks);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load ranks.",
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
