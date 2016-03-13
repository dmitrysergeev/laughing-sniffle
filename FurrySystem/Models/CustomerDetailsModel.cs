using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using FurrySystem.DataLayer;

namespace FurrySystem.Models
{
	public class CustomerDetailsModel
	{
		public Customer Customer { get; set; }
		public IEnumerable<CustomerInternetOrder> CustomerInternetOrders { get; set; }
		public IEnumerable<CustomerPhoneOrder> CustomerPhoneOrders { get; set; }
		public IEnumerable<CustomerTvOrder> CustomerTvOrders { get; set; }
	}

	public class CustomerDetailsModelBuilder
	{
		private readonly ApplicationDbContext dbContext;

		public CustomerDetailsModelBuilder(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public CustomerDetailsModel Build(int customerId)
		{
			var customer = dbContext.Customers.Find(customerId);

			var customerInternetOrders = dbContext.CustomerInternetOrders
				.Include(x => x.Tariff)
				.Include(x => x.Customer)
				.Where(x => x.Customer.Id == customerId)
				.ToList();
			var customerPhoneOrders = dbContext.CustomerPhoneOrders
				.Include(x => x.Tariff)
				.Include(x => x.Customer)
				.Where(x => x.Customer.Id == customerId)
				.ToList();
			var customerTvOrders = dbContext.CustomerTvOrders
				.Include(x => x.Customer)
				.Where(x => x.Customer.Id == customerId)
				.ToList();

			return new CustomerDetailsModel
			{
				Customer = customer,
				CustomerInternetOrders = customerInternetOrders,
				CustomerPhoneOrders = customerPhoneOrders,
				CustomerTvOrders = customerTvOrders
			};
		}
	}
}