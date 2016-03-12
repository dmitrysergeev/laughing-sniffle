using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurrySystem.Models
{
	public class InternetTariff
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int BandwidthLimit { get; set; }
	}
}