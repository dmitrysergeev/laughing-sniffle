using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FurrySystem.Models
{

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base("DefaultConnection")
		{
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<CustomerInternetOrder> CustomerInternetOrders { get; set; }
		public DbSet<CustomerPhoneOrder> CustomerPhoneOrders { get; set; }
		public DbSet<InternetTariff> InternetTariffs { get; set; }
		public DbSet<PhoneTariff> PhoneTariffs { get; set; }
	}
}