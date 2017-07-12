using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulario.Models
{
	public class Rank
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Word> Words { get; set; }
	}
}
