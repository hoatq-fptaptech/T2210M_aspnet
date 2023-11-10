using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using T2210M_MVC.Models;

namespace T2210M_MVC.Controllers;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

}

