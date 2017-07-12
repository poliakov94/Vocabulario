using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Vocabulario.Models
{
	public class Language : BaseDataObject
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Flag { get; set; }
	}
}
