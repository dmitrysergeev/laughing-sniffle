using System.ComponentModel.DataAnnotations;

namespace FurrySystem.DataLayer
{
	public class InternetTariff : Tariff
	{
		[Required]
		public virtual int BandwidthLimit { get; set; }
	}
}