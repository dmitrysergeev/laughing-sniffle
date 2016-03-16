using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FurrySystem.DataLayer;

namespace FurrySystem.Models
{
	public class StatisticsModelBuilder
	{
		private readonly ApplicationDbContext dbContext;
		private readonly UserNamesProvider userNamesProvider;

		public StatisticsModelBuilder(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
			userNamesProvider = new UserNamesProvider(dbContext);
		}

		public StatisticsModel Build(string operatorId, DateTime? startDate, DateTime? endDate)
		{
			return new StatisticsModel
			{
				Users = userNamesProvider.GetUserNamesByIds(),
				OperatorId = operatorId,
				StartDate = startDate,
				EndDate = endDate,
				InternetOrdersData = GetOrderData(dbContext.CustomerInternetOrders, operatorId, startDate, endDate),
				PhoneOrdersData = GetOrderData(dbContext.CustomerPhoneOrders, operatorId, startDate, endDate),
				TvOrdersData = GetOrderData(dbContext.CustomerTvOrders, operatorId, startDate, endDate)
			};
		}

		private OrdersData GetOrderData<T>(DbSet<T> set, string operatorId, DateTime? startDate, DateTime? endDate) where T : CustomerOrder
		{
			var hasOperatorId = !string.IsNullOrEmpty(operatorId);

			var list = (hasOperatorId ? set.Where(x => x.CreateOperatorUserId == operatorId || x.DisableOperatorUserId == operatorId) : set)
				.Where(x=>!startDate.HasValue || x.OrderDate >= startDate.Value)
				.Where(x=>!endDate.HasValue || x.OrderDate <= endDate.Value)
				.AsEnumerable()
				.GroupBy(x => x.OrderDate.Date)
				.OrderBy(x=>x.Key)
				.ToList();

			if(list.Count <= 1)
				list = new List<IGrouping<DateTime, T>>();

			return new OrdersData
			{
				Keys = list.Select(x => x.Key.ToShortDateString()),
				NewOrders = list.Select(x => (double)x.Count(y => !hasOperatorId || y.CreateOperatorUserId == operatorId)).ToList(),
				DisabledOrders = list.Select(x => (double)x.Count(y => y.Disabled && (!hasOperatorId || y.DisableOperatorUserId == operatorId))).ToList()
			};
		}
	}
}