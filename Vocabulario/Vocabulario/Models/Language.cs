﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulario.Models
{
	public class Language : BaseDataObject
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Flag { get; set; }
	}
}
