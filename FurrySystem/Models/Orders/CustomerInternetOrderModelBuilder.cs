using System;
using System.Linq;
using FurrySystem.DataLayer;

namespace FurrySystem.Models.Orders
{
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
				Tariffs = dbContext.InternetTariffs.Where(x=>!x.IsDeleted).ToList()
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