using System.Collections.Generic;
using System.Linq;
using FurrySystem.DataLayer;

namespace FurrySystem.Models
{
	public class UserNamesProvider
	{
		private readonly ApplicationDbContext dbContext;

		public UserNamesProvider(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IEnumerable<KeyValuePair<string, string>> GetUserNamesByIds()
		{
			return new[] {new KeyValuePair<string, string>(null, "All")}
			.Concat(dbContext.Users.ToDictionary(x => x.Id, x => $"{x.FullName} [{x.UserName}]"));
		} 
	}
}