﻿using System.ComponentModel.DataAnnotations;

namespace FurrySystem.Models
{
	public class InternetTariff
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		public virtual int BandwidthLimit { get; set; }
	}
}