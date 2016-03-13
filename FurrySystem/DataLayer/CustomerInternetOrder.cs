using System.ComponentModel.DataAnnotations;

namespace FurrySystem.DataLayer
{
	public class CustomerInternetOrder : CustomerOrder
	{
		[Required]
		public virtual InternetTariff Tariff { get; set; }

		[Required]
		public virtual bool AddRouter { get; set; }
	}
}