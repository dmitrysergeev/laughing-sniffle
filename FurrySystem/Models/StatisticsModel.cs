using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurrySystem.Models
{
	public class StatisticsModel
	{
		public IEnumerable<KeyValuePair<string, string>> Users { get; set; }

		[DisplayName("Start Date")]
		[DataType(DataType.Date)]
		public DateTime? StartDate { get; set; }

		[DisplayName("End Date")]
		[DataType(DataType.Date)]
		public DateTime? EndDate { get; set; }

		[DisplayName("Operator")]
		public string OperatorId { get; set; }
		
		public OrdersData InternetOrdersData { get; set; }
		public OrdersData PhoneOrdersData { get; set; }
		public OrdersData TvOrdersData { get; set; }
	}

	public class OrdersData
	{
		public IEnumerable<string> Keys { get; set; }
		public List<double> NewOrders { get; set; }
		public List<double> DisabledOrders { get; set; }
	}
}