using System.ComponentModel.DataAnnotations;

namespace FurrySystem.DataLayer
{
	public class Tariff
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		public virtual bool IsDeleted { get; set; }
	}
}