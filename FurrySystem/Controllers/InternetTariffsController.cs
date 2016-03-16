using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FurrySystem.DataLayer;

namespace FurrySystem.Controllers
{
	[Authorize]
	public class InternetTariffsController : Controller
	{
		private readonly ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			return View(db.InternetTariffs.ToList());
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var internetTariff = db.InternetTariffs.Find(id);
			if (internetTariff == null)
			{
				return HttpNotFound();
			}
			return View(internetTariff);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanCreate)]
		public ActionResult Create([Bind(Include = "Id,Name,BandwidthLimit,IsDeleted")] InternetTariff internetTariff)
		{
			if (ModelState.IsValid)
			{
				db.InternetTariffs.Add(internetTariff);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(internetTariff);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var internetTariff = db.InternetTariffs.Find(id);
			if (internetTariff == null)
			{
				return HttpNotFound();
			}
			return View(internetTariff);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanCreate)]
		public ActionResult Edit([Bind(Include = "Id,Name,BandwidthLimit,IsDeleted")] InternetTariff internetTariff)
		{
			if (ModelState.IsValid)
			{
				db.Entry(internetTariff).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(internetTariff);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var internetTariff = db.InternetTariffs.Find(id);
			if (internetTariff == null)
			{
				return HttpNotFound();
			}
			return View(internetTariff);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanDelete)]
		public ActionResult DeleteConfirmed(int id)
		{
			var internetTariff = db.InternetTariffs.Find(id);
			internetTariff.IsDeleted = true;
			//db.InternetTariffs.Remove(internetTariff);
			db.SaveChanges();
			return RedirectToAction("Index");
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