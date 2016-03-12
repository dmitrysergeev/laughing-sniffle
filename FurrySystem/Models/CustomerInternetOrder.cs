﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;

namespace FurrySystem.Models
{
	public class CustomerInternetOrder
	{
		[Key]
		public virtual int Id { get; set; }

		[Required]
		public virtual DateTime OrderDate { get; set; }

		[Required]
		public virtual Customer Customer { get; set; }

		[Required]
		public virtual InternetTariff Tariff { get; set; }

		[Required]
		public virtual bool AddRouter { get; set; }

		[Required]
		public virtual bool Disabled { get; set; }
	}

	public class CustomerInternetOrderModel
	{
		public int CustomerId { get; set; }
		public int TariffId { get; set; }
		public bool AddRouter { get; set; }
		public IEnumerable<InternetTariff> Tariffs { get; set; }
	}

	public class CustomerInternetOrderModelBuilder
	{
		private readonly ApplicationDbContext dbContext;

		public CustomerInternetOrderModelBuilder(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public CustomerInternetOrderModel Build(int customerId)
		{
			return new CustomerInternetOrderModel
			{
				CustomerId = customerId,
				Tariffs = dbContext.InternetTariffs.ToList()
			};
		}

		public CustomerInternetOrder BuildCustomerInternetOrder(CustomerInternetOrderModel model)
		{
			var tariff = dbContext.InternetTariffs.Find(model.TariffId);
			var customer = dbContext.Customers.Find(model.CustomerId);

			return new CustomerInternetOrder
			{
				OrderDate = DateTime.Now,
				Tariff = tariff,
				Customer = customer,
				AddRouter = model.AddRouter,
				Disabled = false
			};
		}
	}
}