using System.ComponentModel.DataAnnotations;

namespace FurrySystem.Models
{
	public class PhoneTariff
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		public virtual bool HasLimit { get; set; }
	}
}