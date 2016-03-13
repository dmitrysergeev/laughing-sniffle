using System;
using System.Linq;
using FurrySystem.DataLayer;

namespace FurrySystem.Models.Orders
{
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
				Tariffs = dbContext.PhoneTariffs.Where(x => !x.IsDeleted).ToList()
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