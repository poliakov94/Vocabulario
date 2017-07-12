using System;
using Vocabulario.Helpers;
using SQLite;

namespace Vocabulario.Models
{
	public class BaseDataObject : ObservableObject
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}
