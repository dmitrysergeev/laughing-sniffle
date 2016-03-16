using FurrySystem.Authorization;
using FurrySystem.DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FurrySystem.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			//Will be created users CanCreate : psw_CanCreate, CanDelete : psw_CanDelete
			//AddUserAndRole(context, Roles.CanCreate);
			//AddUserAndRole(context, Roles.CanDelete);

			//Will be created user Admin : psw_Admin
			//AddAdminUser(context);
		}

		private bool AddUserAndRole(ApplicationDbContext context, string role)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			roleManager.Create(new IdentityRole(role));

			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var user = new ApplicationUser
			{
				UserName = role,
				FullName = role
			};
			var identityResult = userManager.Create(user, "psw_" + role);
			if (identityResult.Succeeded == false)
				return identityResult.Succeeded;
			identityResult = userManager.AddToRole(user.Id, Roles.CanCreate);
			return identityResult.Succeeded;
		}

		private bool AddAdminUser(ApplicationDbContext context)
		{
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var user = new ApplicationUser
			{
				UserName = "Admin",
				FullName = "Administrator"
			};
			var identityResult = userManager.Create(user, "psw_Admin");
			if (identityResult.Succeeded == false)
				return identityResult.Succeeded;
			identityResult = userManager.AddToRole(user.Id, Roles.CanCreate);
			if (identityResult.Succeeded == false)
				return identityResult.Succeeded;
			identityResult = userManager.AddToRole(user.Id, Roles.CanDelete);
			return identityResult.Succeeded;
		}
	}
}
