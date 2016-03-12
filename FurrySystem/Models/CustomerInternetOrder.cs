using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurrySystem.Models
{
	public class CustomerInternetOrder
	{
		public int Id { get; set; }
		public Customer Customer { get; set; }
		public InternetTariff Tariff { get; set; }
		public bool AddRouter { get; set; }
		public bool Disabled { get; set; }
	}
}