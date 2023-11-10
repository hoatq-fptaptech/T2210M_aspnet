using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T2210M_MVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2210M_MVC.Controllers
{
    public class AuthController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<UserRegisterModel> ls = new List<UserRegisterModel>();
            ls.Add(new UserRegisterModel
            {
                Id=1,
                Email = "Nam@gmail.com",
                FullName="Nguyen Van Nam",
                Telephone="0987654321"
            });
            ls.Add(new UserRegisterModel
            {
                Id = 2,
                Email = "Duy@gmail.com",
                FullName = "Nguyen Van Duy",
                Telephone = "0987654322"
            });
            //ViewData["users"] = ls;
            //ViewBag.users = ls;
            return View(ls);
        }
    }
}

