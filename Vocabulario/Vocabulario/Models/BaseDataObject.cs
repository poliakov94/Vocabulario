﻿using System;
using Vocabulario.Helpers;

namespace Vocabulario.Models
{
	public class BaseDataObject : ObservableObject
	{
		/// <summary>
		/// Azure created at time stamp
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Azure UpdateAt timestamp for online/offline sync
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Azure version for online/offline sync
		/// </summary>
		public string AzureVersion { get; set; }
	}
}
