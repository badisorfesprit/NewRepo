using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Solution.Service;
using Solution.Web.Models;

namespace Solution.Web.Controllers
{
    public class HomeController : Controller
    {

        UserService us = new UserService();

        public ActionResult Index()
        {
            var users = us.GetMany();
            List<UserVM> list = new List<UserVM>();
            foreach (var user in users)
            {
                var u = new UserVM();
                u.DateOfBirth = user.DateOfBirth;
                u.FirstName = user.FirstName;
                u.id = user.Id;
                u.LastName = user.LastName;
                list.Add(u);
            }
            return View(list);
        }
       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}