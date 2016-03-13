using System.ComponentModel.DataAnnotations;

namespace FurrySystem.DataLayer
{
	public class PhoneTariff : Tariff
	{
		[Required]
		public virtual bool HasLimit { get; set; }
	}
}