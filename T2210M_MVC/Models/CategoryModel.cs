using System;
using System.ComponentModel.DataAnnotations;
namespace T2210M_MVC.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Vui lòng nhập tên")]
		[MinLength(6,ErrorMessage = "Tên danh mục tối thiểu 6 ký tự")]
		public string Name { get; set; }
	}
}

