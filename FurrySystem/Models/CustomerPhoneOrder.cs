using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;

namespace FurrySystem.Models
{
	public class CustomerPhoneOrder
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual DateTime OrderDate { get; set; }

		[Required]
		public virtual Customer Customer { get; set; }

		[Required]
		public virtual PhoneTariff Tariff { get; set; }

		[Required]
		public virtual bool IsIpTelephone { get; set; }

		[Required]
		public virtual bool Disabled { get; set; }
	}

	public class CustomerPhoneOrderModel
	{
		public int CustomerId { get; set; }
		public int TariffId { get; set; }
		public bool IsIpTelephone { get; set; }
		public IEnumerable<PhoneTariff> Tariffs { get; set; }
	}

	public class CustomerPhoneOrderModelBuilder
	{
		private readonly ApplicationDbContext dbContext;

		public CustomerPhoneOrderModelBuilder(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public CustomerPhoneOrderModel Build(int customerId)
		{
			return new CustomerPhoneOrderModel
			{
				CustomerId = customerId,
				Tariffs = dbContext.PhoneTariffs.ToList()
			};
		}

		public CustomerPhoneOrder BuildCustomerPhoneOrder(CustomerPhoneOrderModel model)
		{
			var tariff = dbContext.PhoneTariffs.Find(model.TariffId);
			var customer = dbContext.Customers.Find(model.CustomerId);

			return new CustomerPhoneOrder
			{
				OrderDate = DateTime.Now,
				Tariff = tariff,
				Customer = customer,
				IsIpTelephone = model.IsIpTelephone,
				Disabled = false
			};
		}
	}
}