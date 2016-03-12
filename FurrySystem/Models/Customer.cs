using System.ComponentModel.DataAnnotations;

namespace FurrySystem.Models
{
	public class Customer
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual string Inn { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		public virtual string Activity { get; set; }

		[Required]
		public virtual string ContactPerson { get; set; }

		[Required]
		public virtual string Email { get; set; }

		[Required]
		public virtual string Address { get; set; }

		[Required]
		public virtual string LegalAddress { get; set; }

		[Required]
		public virtual string HeadPosition { get; set; }

		[Required]
		public virtual string Fio { get; set; }

		[Required]
		public virtual bool IsDeleted { get; set; }
	}
}