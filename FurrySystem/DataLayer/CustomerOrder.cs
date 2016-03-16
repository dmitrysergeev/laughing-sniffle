using System;
using System.ComponentModel.DataAnnotations;

namespace FurrySystem.DataLayer
{
	public class CustomerOrder
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual DateTime OrderDate { get; set; }

		[Required]
		public virtual Customer Customer { get; set; }

		[Required]
		public virtual bool Disabled { get; set; }

		public virtual DateTime? DisabledDate { get; set; }

		[Required]
		public virtual string CreateOperatorUserId { get; set; }

		public virtual string DisableOperatorUserId { get; set; }
	}
}