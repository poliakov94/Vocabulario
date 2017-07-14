using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulario.Models
{
	public class Language : BaseDataObject
	{
		[JsonProperty(PropertyName = "Name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "Flag")]
		public string Flag { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public List<Rank> Ranks { get; set; }
	}
}
