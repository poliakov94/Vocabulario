using Newtonsoft.Json;
using System;
using Vocabulario.Helpers;

namespace Vocabulario.Models
{
	public class BaseDataObject : ObservableObject
	{
		[JsonProperty(PropertyName = "id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "DateUtc")]
		public DateTime DateUtc { get; set; } = DateTime.UtcNow;

		[Microsoft.WindowsAzure.MobileServices.Version]
		public string AzureVersion { get; set; }
				
		[Newtonsoft.Json.JsonIgnore]
		public string DateDisplay { get { return DateUtc.ToLocalTime().ToString("d"); } }

		[Newtonsoft.Json.JsonIgnore]
		public string TimeDisplay { get { return DateUtc.ToLocalTime().ToString("t"); } }		
	}
}
