using System.Collections.Generic;
using FurrySystem.DataLayer;

namespace FurrySystem.Models.Orders
{
	public class CustomerInternetOrderModel : CustomerOrderModel
	{
		public int TariffId { get; set; }
		public bool AddRouter { get; set; }
		public IEnumerable<InternetTariff> Tariffs { get; set; }
	}
}