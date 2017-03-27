using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VSMS.Data;

namespace vsms.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public Boolean isAdmin()
        {
            if((string)Session["userType"] == "Admin")
                return true;
            else
                return false;
        }

        public Boolean isLoggedIn()
        {
            if (Session["userId"] != null)
                return true;
            else
                return false;
        }

        public ActionResult Login() {
            if(!isLoggedIn())
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoggedIn");
            }
        }

        [HttpPost]
        public ActionResult Login(user account)
        {

            using (VSMSContext db = new VSMSContext())
            {
                var usr = db.users.SingleOrDefault(u => u.userName == account.userName && u.userPass == account.userPass);
                if (usr != null)
                {
                    Session["userId"] = usr.userId.ToString();
                    Session["userName"] = usr.userName.ToString();
                    Session["userType"] = usr.userType.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is Wrong!");
                }
                return View();

            }
        }
            public ActionResult LoggedIn()
            {
                if (isLoggedIn())
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            
            }

            public ActionResult Logout()
            {
                Session["userId"] = null;
                Session["userName"] = null;
                Session["userType"] = null;

                return RedirectToAction("Login");
            }

          
            public ActionResult CreateEmployee()
            {
                if(isLoggedIn() && isAdmin())
                {
                    return View();
                }
                return RedirectToAction("LoggedIn");
            }


            [HttpPost]
            public ActionResult CreateEmployee(user em)
            {
                if (ModelState.IsValid)
                {
                    using (VSMSContext con = new VSMSContext())
                    {
                        con.users.Add(em);
                        con.SaveChanges();
                    }

                    return RedirectToAction("UserList");
                }
                return View();

            }

            public ActionResult UserList()
            {
                if(isLoggedIn() && isAdmin())
                {
                    using (VSMSContext db = new VSMSContext())
                    {
                        return View(db.users.ToList());
                    }
                }
                return RedirectToAction("LoggedIn");
            }

            [HttpGet]
            public ActionResult Delete(int id)
            {
                if(isLoggedIn() && isAdmin())
                {
                    using (VSMSContext db = new VSMSContext())
                    {
                        user user1 = db.users.SingleOrDefault(u => u.userId == id);
                        return View(user1);
                    }
                }
                return RedirectToAction("LoggedIn");
            }



            [HttpPost][ActionName("Delete")]
            public ActionResult ConfirmDelete(int id)
            {
                using (VSMSContext db = new VSMSContext())
                {
                    user user1 = db.users.SingleOrDefault(u => u.userId == id);
                    db.users.Remove(user1);
                    db.SaveChanges();
                }
                return RedirectToAction("UserList");
            }

            public ActionResult MostProfit()
            {
                using (VSMSContext context = new VSMSContext())
                {
                    ArrayList xData = new ArrayList();
                    ArrayList yData = new ArrayList();
                    var result = context.vehicles
                                                .Where(o => o.status == "Sold")
                                                .GroupBy(o => o.vehiclebrand)
                                                .Select(s => new { Model = s.Key, Sum = s.Sum(x => x.sprice - x.bprice)});
                                                
                    result.ToList().ForEach(v => xData.Add(v.Model));
                    result.ToList().ForEach(v => yData.Add(v.Sum));
                    var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla3D)
                                .AddTitle("Total Profit")
                                .AddSeries("Default", chartType: "Pie",
                                    xValue: xData, xField: "Model",
                                    yValues: yData, yFields: "Price")
                                .Write();
                }
                return null; 
            }

            public ActionResult BoughtvsSold()
            {
                DateTime upperDate = DateTime.Today;
                DateTime lowerDate = DateTime.Today.AddMonths(-6);
                using (VSMSContext context = new VSMSContext())
                {
                    ArrayList xData = new ArrayList();
                    ArrayList yData = new ArrayList();
                    var result = context.vehicles
                                                .Where(o => (o.bdate >= lowerDate && o.bdate <= upperDate))
                                                .Where(o => o.status == "Available")
                                                .Sum(o=>o.bprice);

                    xData.Add("Investment");
                    yData.Add((decimal)result);
                    result = context.vehicles
                                            .Where(o => (o.bdate >= lowerDate && o.bdate <= upperDate))
                                            .Where(o => o.status == "Sold")
                                            .Sum(o => o.bprice);

                    xData.Add("Sales");
                    yData.Add((decimal)result);
                    var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                                .AddTitle("Bought vs. Sold(Last 6 months)")
                                .AddSeries("Default",
                                    xValue: xData, xField: "Model",
                                    yValues: yData, yFields: "Price")
                                .Write();
                }
                return null;
            }
       }
}
