using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulario.Models;

namespace Vocabulario.ViewModels
{
	public class LanguageDetailViewModel : BaseViewModel
	{
		public Language Language { get; set; }

		public LanguageDetailViewModel(Language language = null)
		{
			Title = language.Name;
			Language = language;
		}
	}
}
