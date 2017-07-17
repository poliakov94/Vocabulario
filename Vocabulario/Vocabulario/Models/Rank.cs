using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulario.Models
{
	public class Rank : BaseDataObject
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string LanguageID { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public List<Word> Words { get; set; }
	}
}
