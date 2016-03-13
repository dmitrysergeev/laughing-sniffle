using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FurrySystem.DataLayer;
using FurrySystem.Models;

namespace FurrySystem.Controllers
{
	[Authorize]
	public class StatisticsController : Controller
	{
		private readonly ApplicationDbContext db = new ApplicationDbContext();
		private readonly StatisticsModelBuilder statisticsModelBuilder;

		public StatisticsController()
		{
			statisticsModelBuilder = new StatisticsModelBuilder(db);
		}

		public ActionResult Index(string operatorId, DateTime? startDate, DateTime? endDate)
		{
			var model = statisticsModelBuilder.Build(operatorId, startDate, endDate);
			return View(model);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}