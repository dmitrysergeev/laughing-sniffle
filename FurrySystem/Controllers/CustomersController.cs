using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using FurrySystem.DataLayer;
using FurrySystem.Models;

namespace FurrySystem.Controllers
{
	[Authorize]
	public class CustomersController : Controller
	{
		private readonly ApplicationDbContext db = new ApplicationDbContext();
		private readonly CustomerDetailsModelBuilder customerDetailsModelBuilder;

		public CustomersController()
		{
			customerDetailsModelBuilder = new CustomerDetailsModelBuilder(db);
		}

		public ActionResult Index(CustomersSearch.Type? searchType, string query)
		{
			var customers = db.Customers.ToList();

			if(!searchType.HasValue || searchType.Value == CustomersSearch.Type.None || string.IsNullOrEmpty(query))
				return View(customers);

			return View(CustomersSearch.FilterCustomers(customers, searchType.Value, query));
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var model = customerDetailsModelBuilder.Build(id.Value);
			if (model.Customer == null)
			{
				return HttpNotFound();
			}
			return View(model);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanCreate)]
		public ActionResult Create([Bind(Include = "Id,Inn,Name,Activity,ContactPerson,Email,Address,LegalAddress,HeadPosition,Fio")] Customer customer)
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
			var customer = db.Customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanCreate)]
		public ActionResult Edit([Bind(Include = "Id,Inn,Name,Activity,ContactPerson,Email,Address,LegalAddress,HeadPosition,Fio")] Customer customer)
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
			var customer = db.Customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Authorization.Roles.CanDelete)]
		public ActionResult DeleteConfirmed(int id)
		{
			var customer = db.Customers.Find(id);
			customer.IsDeleted = true;
			//db.Customers.Remove(customer);
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