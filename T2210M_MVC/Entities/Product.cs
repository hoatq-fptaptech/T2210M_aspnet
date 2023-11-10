using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace T2210M_MVC.Entities
{
	[Table("products")]
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		[Required]
		public double Price { get; set; }

		[Column(TypeName ="text")]
		public string Description { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
	}
}

