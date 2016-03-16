using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FurrySystem.DataLayer;

namespace FurrySystem.Controllers
{
	[Authorize]
	public class PhoneTariffsController : Controller
	{
		private readonly ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			return View(db.PhoneTariffs.ToList());
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var phoneTariff = db.PhoneTariffs.Find(id);
			if (phoneTariff == null)
			{
				return HttpNotFound();
			}
			return View(phoneTariff);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanCreate)]
		public ActionResult Create([Bind(Include = "Id,Name,HasLimit,IsDeleted")] PhoneTariff phoneTariff)
		{
			if (ModelState.IsValid)
			{
				db.PhoneTariffs.Add(phoneTariff);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(phoneTariff);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var phoneTariff = db.PhoneTariffs.Find(id);
			if (phoneTariff == null)
			{
				return HttpNotFound();
			}
			return View(phoneTariff);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanCreate)]
		public ActionResult Edit([Bind(Include = "Id,Name,HasLimit,IsDeleted")] PhoneTariff phoneTariff)
		{
			if (ModelState.IsValid)
			{
				db.Entry(phoneTariff).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(phoneTariff);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var phoneTariff = db.PhoneTariffs.Find(id);
			if (phoneTariff == null)
			{
				return HttpNotFound();
			}
			return View(phoneTariff);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanDelete)]
		public ActionResult DeleteConfirmed(int id)
		{
			var phoneTariff = db.PhoneTariffs.Find(id);
			phoneTariff.IsDeleted = true;
			//db.PhoneTariffs.Remove(phoneTariff);
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