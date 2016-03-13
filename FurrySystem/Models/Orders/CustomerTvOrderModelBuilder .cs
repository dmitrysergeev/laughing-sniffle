using System;
using FurrySystem.DataLayer;

namespace FurrySystem.Models.Orders
{
	public class CustomerTvOrderModelBuilder
	{
		private readonly ApplicationDbContext dbContext;

		public CustomerTvOrderModelBuilder(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public CustomerTvOrderModel Build(int customerId)
		{
			return new CustomerTvOrderModel
			{
				CustomerId = customerId
			};
		}

		public CustomerTvOrder BuildCustomerTvOrder(CustomerTvOrderModel model)
		{
			var customer = dbContext.Customers.Find(model.CustomerId);

			return new CustomerTvOrder
			{
				OrderDate = DateTime.Now,
				Customer = customer,
				Disabled = false
			};
		}
	}
}