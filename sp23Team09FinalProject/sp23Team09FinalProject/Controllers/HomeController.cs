using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject.Controllers
{

  
    public class HomeController : Controller
    {
        private AppDbContext _context;


        public HomeController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
           
            return View();
        } 
    }
}