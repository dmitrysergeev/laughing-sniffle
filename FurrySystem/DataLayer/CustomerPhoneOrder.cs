using System.ComponentModel.DataAnnotations;

namespace FurrySystem.DataLayer
{
	public class CustomerPhoneOrder : CustomerOrder
	{
		[Required]
		public virtual PhoneTariff Tariff { get; set; }

		[Required]
		public virtual bool IsIpTelephone { get; set; }
	}
}