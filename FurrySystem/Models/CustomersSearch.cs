using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FurrySystem.Models
{
	public static class CustomersSearch
	{
		public enum Type
		{
			None = 0,
			Inn =  1,
			Fio = 2,
			Address = 3
		}

		private static readonly Dictionary<Type, PropertyInfo> members = new Dictionary<Type, PropertyInfo>
		{
			{ Type.Inn, typeof(Customer).GetProperty(nameof(Customer.Inn)) },
			{ Type.Fio, typeof(Customer).GetProperty(nameof(Customer.Fio)) },
			{ Type.Address, typeof(Customer).GetProperty(nameof(Customer.Address)) }
		};

		public static readonly Dictionary<Type, string> Names = new Dictionary<Type, string>
		{
			{ Type.Inn, "Inn" },
			{ Type.Fio, "Fio" },
			{ Type.Address, "Address" }
		};

		public static IEnumerable<Customer> FilterCustomers(IEnumerable<Customer> customers, Type type, string query)
		{
			return customers.Where(x => ((string) members[type].GetValue(x)).StartsWith(query));
		}
	}
}