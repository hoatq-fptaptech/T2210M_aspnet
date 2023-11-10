using System;
namespace T2210M_MVC.Models
{
	public class UserRegisterModel
	{
		public int Id { get; set; } // abstract property
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Telephone { get; set; }
	}
}

