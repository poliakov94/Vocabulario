using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulario.Models
{
	public class Word : BaseDataObject
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string RankID { get; set; }
	}
}
