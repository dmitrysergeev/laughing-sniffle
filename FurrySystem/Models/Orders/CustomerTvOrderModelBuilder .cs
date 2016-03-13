using System;
using System.Web;
using FurrySystem.DataLayer;
using Microsoft.AspNet.Identity;

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
			var operatorId = HttpContext.Current.User.Identity.GetUserId();

			return new CustomerTvOrder
			{
				OrderDate = DateTime.Now,
				Customer = customer,
				Disabled = false,
				OperatorUserId = operatorId
			};
		}
	}
}