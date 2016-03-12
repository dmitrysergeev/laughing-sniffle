using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FurrySystem.Models;

namespace FurrySystem.Controllers
{
	[Authorize]
	public class CustomersController : Controller
	{
		private readonly ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			return View(db.Customers.ToList());
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var contractor = db.Customers.Find(id);
			if (contractor == null)
			{
				return HttpNotFound();
			}
			return View(contractor);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(
			[Bind(Include = "ID,Inn,Name,Activity,ContactPerson,Email,Address,LegalAddress,HeadPosition,Fio")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				db.Customers.Add(customer);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(customer);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var contractor = db.Customers.Find(id);
			if (contractor == null)
			{
				return HttpNotFound();
			}
			return View(contractor);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(
			[Bind(Include = "ID,Inn,Name,Activity,ContactPerson,Email,Address,LegalAddress,HeadPosition,Fio")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				db.Entry(customer).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(customer);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var contractor = db.Customers.Find(id);
			if (contractor == null)
			{
				return HttpNotFound();
			}
			return View(contractor);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var contractor = db.Customers.Find(id);
			db.Customers.Remove(contractor);
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