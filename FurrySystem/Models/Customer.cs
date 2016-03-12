namespace FurrySystem.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Inn { get; set; }
		public string Name { get; set; }
		public string Activity { get; set; }
		public string ContactPerson { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string LegalAddress { get; set; }
		public string HeadPosition { get; set; }
		public string Fio { get; set; }
		public bool IsDeleted { get; set; }
	}
}