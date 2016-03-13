using System.Collections.Generic;
using FurrySystem.DataLayer;

namespace FurrySystem.Models.Orders
{
	public class CustomerPhoneOrderModel : CustomerOrderModel
	{
		public int TariffId { get; set; }
		public bool IsIpTelephone { get; set; }
		public IEnumerable<PhoneTariff> Tariffs { get; set; }
	}
}