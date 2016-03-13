﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FurrySystem.DataLayer;
using FurrySystem.Models.Orders;

namespace FurrySystem.Controllers
{
	[Authorize(Roles = Authorization.Roles.CanEdit)]
	public class CustomersOrdersController : Controller
	{
		private readonly ApplicationDbContext db = new ApplicationDbContext();
		private readonly CustomerInternetOrderModelBuilder internetOrderModelBuilder;
		private readonly CustomerPhoneOrderModelBuilder phoneOrderModelBuilder;
		private readonly CustomerTvOrderModelBuilder tvOrderModelBuilder;

		public CustomersOrdersController()
		{
			internetOrderModelBuilder = new CustomerInternetOrderModelBuilder(db);
			phoneOrderModelBuilder = new CustomerPhoneOrderModelBuilder(db);
			tvOrderModelBuilder = new CustomerTvOrderModelBuilder(db);
		}

		#region Internet
		public ActionResult CreateInternetOrder(int? id)
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
			var model = internetOrderModelBuilder.Build(id.Value);
			return View("CreateInternetOrder", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateInternetOrder([Bind(Include = "CustomerId,TariffId,AddRouter")] CustomerInternetOrderModel model)
		{
			if (ModelState.IsValid)
			{
				var order = internetOrderModelBuilder.BuildCustomerInternetOrder(model);
				db.CustomerInternetOrders.Add(order);
				db.SaveChanges();
			}

			return RedirectToAction("Details", "Customers", new {id = model.CustomerId});
		}

		[HttpPost, ActionName("DeleteInternetOrder")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteInternetOrder(int? id, int? customerId)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var order = db.CustomerInternetOrders
				.Include(x => x.Customer)
				.Include(x => x.Tariff)
				.FirstOrDefault(x => x.Id == id);
			if (order == null)
			{
				return HttpNotFound();
			}

			order.Disabled = true;
			order.DisabledDate = DateTime.Now;
			db.SaveChanges();

			if (customerId.HasValue)
				return RedirectToAction("Details", "Customers", new {id = customerId});

			return RedirectToAction("Index", "Customers");
		}
		#endregion


		#region Phone
		public ActionResult CreatePhoneOrder(int? id)
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
			var model = phoneOrderModelBuilder.Build(id.Value);
			return View("CreatePhoneOrder", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreatePhoneOrder([Bind(Include = "CustomerId,TariffId,IsIpTelephone")] CustomerPhoneOrderModel model)
		{
			if (ModelState.IsValid)
			{
				var order = phoneOrderModelBuilder.BuildCustomerPhoneOrder(model);
				db.CustomerPhoneOrders.Add(order);
				db.SaveChanges();
			}

			return RedirectToAction("Details", "Customers", new { id = model.CustomerId });
		}

		[HttpPost, ActionName("DeletePhoneOrder")]
		[ValidateAntiForgeryToken]
		public ActionResult DeletePhoneOrder(int? id, int? customerId)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var order = db.CustomerPhoneOrders
				.Include(x => x.Customer)
				.Include(x => x.Tariff)
				.FirstOrDefault(x => x.Id == id);
			if (order == null)
			{
				return HttpNotFound();
			}

			order.Disabled = true;
			order.DisabledDate = DateTime.Now;
			//db.CustomerPhoneOrders.Attach(order);
			//db.Entry(order).Property(x => x.Disabled).IsModified = true;
			db.SaveChanges();

			if (customerId.HasValue)
				return RedirectToAction("Details", "Customers", new { id = customerId });

			return RedirectToAction("Index", "Customers");
		}

		#endregion


		#region Tv
		public ActionResult CreateTvOrder(int? id)
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
			var model = tvOrderModelBuilder.Build(id.Value);
			return View("CreateTvOrder", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateTvOrder([Bind(Include = "CustomerId")] CustomerTvOrderModel model)
		{
			if (ModelState.IsValid)
			{
				var order = tvOrderModelBuilder.BuildCustomerTvOrder(model);
				db.CustomerTvOrders.Add(order);
				db.SaveChanges();
			}

			return RedirectToAction("Details", "Customers", new { id = model.CustomerId });
		}

		[HttpPost, ActionName("DeleteTvOrder")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteTvOrder(int? id, int? customerId)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var order = db.CustomerTvOrders
				.Include(x => x.Customer)
				.FirstOrDefault(x => x.Id == id);
			if (order == null)
			{
				return HttpNotFound();
			}

			order.Disabled = true;
			order.DisabledDate = DateTime.Now;
			//db.CustomerPhoneOrders.Attach(order);
			//db.Entry(order).Property(x => x.Disabled).IsModified = true;
			db.SaveChanges();

			if (customerId.HasValue)
				return RedirectToAction("Details", "Customers", new { id = customerId });

			return RedirectToAction("Index", "Customers");
		}

		#endregion


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