using System;
using System.ComponentModel.DataAnnotations;
namespace T2210M_API.Models
{
	public class CategoryModel
	{
		public int id { get; set; }
		[Required]
		public string name { get; set; }
	}
}

