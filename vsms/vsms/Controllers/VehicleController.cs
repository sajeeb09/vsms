using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VSMS.Data;

namespace vsms.Controllers
{
    public class VehicleController : Controller
    {
        //
        // GET: /Vehicle/
        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                using (VSMSContext db = new VSMSContext())
                {
                    return View(db.vehicles.ToList());
                }
            }
            else
            {
                return RedirectToAction("../User/LoggedIn");
            }
        }

        public Boolean isAdmin()
        {
            if ((string)Session["userType"] == "Admin")
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

        [HttpGet]
        public ActionResult CreateVehicle()
        {
            if (isLoggedIn())
            {
                using (VSMSContext db = new VSMSContext())
                {
                    ViewBag.man = new SelectList(db.manufacturars.ToList(), "vehiclemanufacturar", "vehiclemanufacturar");
                    ViewBag.mod = new SelectList(db.brands.ToList(), "vehiclebrand", "vehiclebrand");

                }
                return View();
            }
            else
            {
                return RedirectToAction("../User/LoggedIn");
            }
        }

        [HttpPost]
        public ActionResult CreateVehicle(vehicle v)
        {
            if (ModelState.IsValid)
            {
                using (VSMSContext db = new VSMSContext())
                {
                    
                    db.vehicles.Add(v);
                    v.price = v.bprice;
                    v.sprice = 0;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult CreateManufacturar()
        {
            if (isLoggedIn())
            {
                using (VSMSContext db = new VSMSContext())
                {
                   
                    return View();
                }
            }
            else
            {
                return RedirectToAction("../User/LoggedIn");
            }
        }

        [HttpPost]
        public ActionResult CreateManufacturar(manufacturar man)
        {
           
            if (ModelState.IsValid)
            {
                using (VSMSContext db = new VSMSContext())
                {
                        var tag = db.manufacturars.SingleOrDefault(m => m.vehiclemanufacturar == man.vehiclemanufacturar);
                        if (tag == null)
                        {
                            db.manufacturars.Add(man);
                            db.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("vehiclemanufacturar", "Already Exists!");
                            return View(man);
                        }
                   
                }
                return RedirectToAction("Manufacturar");
            }
            return View();
        }


        [HttpGet]
        public ActionResult CreateBrand()
        {
            if (isLoggedIn())
            {
                using (VSMSContext db = new VSMSContext())
                {
                    //ViewBag.dropdownManufacturar = db.manufacturars.ToList();
                    ViewBag.manuf = new SelectList(db.manufacturars.ToList(), "manufacturarId", "vehiclemanufacturar");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("../User/LoggedIn");
            }
        }

        [HttpPost]
        public ActionResult CreateBrand(brand b)
        {
            using (VSMSContext db = new VSMSContext())
            {
                ViewBag.manuf = new SelectList(db.manufacturars.ToList(), "manufacturarId", "vehiclemanufacturar");
                var tag = db.brands.SingleOrDefault(m => m.vehiclebrand == b.vehiclebrand);
                if (tag == null)
                {
                    db.brands.Add(b);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("vehiclebrand", "Already Exists!");
                    return View(b);
                }

            }
            return RedirectToAction("Brand");
        }
       
        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    vehicle v = context.vehicles.SingleOrDefault(d => d.vehicleId == id);
                    ViewBag.man = new SelectList(context.manufacturars.ToList(), "vehiclemanufacturar", "vehiclemanufacturar");
                    ViewBag.mod = new SelectList(context.brands.ToList(), "vehiclebrand", "vehiclebrand");
                    return View(v);
                }
            }
            else
            {
                return RedirectToAction("../User/LoggedIn");
            }
        }

        [HttpPost]
        public ActionResult EditVehicle([Bind(Exclude = "sprice")]  vehicle v)
        {   
            using (VSMSContext context = new VSMSContext())
            {
                ModelState.Remove("sprice");

                if(ModelState.IsValid)
                {
                    v.price = v.sprice;
                    context.Entry(v).State = EntityState.Modified;
                    
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    vehicle v = context.vehicles.SingleOrDefault(d => d.vehicleId == id);
                    return View(v);
                }
            }
            else
                return RedirectToAction("LoggedIn");
        }

        public ActionResult DeleteVehicle(int id)
        {
            if (isLoggedIn() && isAdmin())
            {
                using (VSMSContext db = new VSMSContext())
                {
                    vehicle v = db.vehicles.SingleOrDefault(u => u.vehicleId == id);
                    return View(v);
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        [HttpPost] [ActionName("DeleteVehicle")]
        public ActionResult DeleteVehicleConfirmed(int id)
        {
            using (VSMSContext db = new VSMSContext())
            {
                vehicle v = db.vehicles.SingleOrDefault(u => u.vehicleId == id);
                customer c = db.customers.SingleOrDefault(d => d.vehicleId == v.vehicleId);
                if(c != null)
                {
                    db.customers.Remove(c);
                }
                db.vehicles.Remove(v);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Manufacturar()
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    return View(context.manufacturars.ToList());
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        public ActionResult EditManufacturar(int id)
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    manufacturar m = context.manufacturars.SingleOrDefault(d => d.manufacturarId == id);
                    return View(m);
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        [HttpPost]
        public ActionResult EditManufacturar(manufacturar m)
        {
            using (VSMSContext context = new VSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(m).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Manufacturar");
                }
            }
            return View();
        }

        public ActionResult DeleteManufacturar(int id)
        {
            if (isLoggedIn() && isAdmin())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    manufacturar v = context.manufacturars.SingleOrDefault(u => u.manufacturarId == id);
                    return View(v);
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        [HttpPost] [ActionName("DeleteManufacturar")]
        public ActionResult DeleteManufacturarConfirmed(int id)
        {
            using (VSMSContext context = new VSMSContext())
            {
                manufacturar v = context.manufacturars.SingleOrDefault(u => u.manufacturarId == id);
                context.manufacturars.Remove(v);
                context.SaveChanges();
            }
            return RedirectToAction("Manufacturar");
        }

        public ActionResult Brand()
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    return View(context.brands.ToList());
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        public ActionResult EditBrand(int id)
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    ViewBag.manu = new SelectList(context.manufacturars.ToList(), "manufacturarId", "vehiclemanufacturar");
                    brand m = context.brands.SingleOrDefault(d => d.brandId == id);
                    return View(m);
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        [HttpPost]
        public ActionResult EditBrand(brand m)
        {
            using (VSMSContext context = new VSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(m).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Brand");
                }
                return View();
            }
        }

        public ActionResult DeleteBrand(int id)
        {
            if (isLoggedIn() && isAdmin())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    brand v = context.brands.SingleOrDefault(u => u.brandId == id);
                    return View(v);
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        [HttpPost]
        [ActionName("DeleteBrand")]
        public ActionResult DeleteBrandConfirmed(int id)
        {
            using (VSMSContext context = new VSMSContext())
            {
                brand v = context.brands.SingleOrDefault(u => u.brandId == id);
                context.brands.Remove(v);
                context.SaveChanges();
            }
            return RedirectToAction("Brand");
        }

        [HttpGet]
        public ActionResult Sell(int id)
        {
            if (isLoggedIn())
            {
                using (VSMSContext context = new VSMSContext())
                {
                    vehicle v = context.vehicles.SingleOrDefault(d => d.vehicleId == id);
                    if (v.status == "Available")
                    {
                        ViewBag.VehicleId = v.vehicleId;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
                return RedirectToAction("~/Views/User/LoggedIn");
        }

        [HttpPost]
        public ActionResult Sell(customer c, int id)
        {
            using(VSMSContext context = new VSMSContext())
            {
                context.customers.Add(c);
                var car = context.vehicles.Single(v => v.vehicleId == id);
                car.status = "Sold";
                car.sprice = c.sprice;
                car.price = c.sprice;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Customer()
        {
            if (isLoggedIn())
            {
                using(VSMSContext context = new VSMSContext())
                {
                    return View(context.customers.ToList());
                }
            }
            else
                return RedirectToAction("../User/LoggedIn");
        }

        public ActionResult VehicleDetails(int id)
        {
            if(isLoggedIn())
            {
                using(VSMSContext context = new VSMSContext())
                {
                    vehicle v = context.vehicles.SingleOrDefault(d => d.vehicleId == id);
                    return View(v);
                }
            }
            return RedirectToAction("../User/LoggedIn");
        }

        public ActionResult DeleteCustomer(int id)
        {
            if(isLoggedIn() && isAdmin())
            {
                using(VSMSContext context = new VSMSContext())
                {
                    customer c = context.customers.SingleOrDefault(d => d.customerId == id);
                    return View(c);
                }
            }
            return RedirectToAction("../User/LoggedIn");
        }


        [HttpPost] [ActionName("DeleteCustomer")]
        public ActionResult DeleteCustomerConfirmed(int id)
        {
            using(VSMSContext context = new VSMSContext())
            {
                customer c = context.customers.SingleOrDefault(d => d.customerId == id);
                vehicle v = context.vehicles.SingleOrDefault(d => d.vehicleId == c.vehicleId);
                v.status = "Available";
                v.sprice = 0;
                context.customers.Remove(c);
                context.SaveChanges();
            }
            return RedirectToAction("Customer");
        }

        public ActionResult GetBestSelling()
        {
            if (isLoggedIn() && isAdmin())
            {
                using (VSMSContext db = new VSMSContext())
                {
                    //var all = db.vehicles.SqlQuery("SELECT vehiclemanufacturar, COUNT(1) FROM vehicles GROUP BY vehiclemanufacturar").ToList(); 
                    return View();
                    //vehicle v = context.vehicles
                    //return View(v);
                }
            }
            return RedirectToAction("../User/LoggedIn");
        }

        public ActionResult Sold()
        {
            if(isLoggedIn())
            {
                using(VSMSContext context = new VSMSContext())
                {
                    List<vehicle> sold = (from v in context.vehicles where v.status == "Sold" select v).ToList();
                    return View(sold);
                }
            }
            return RedirectToAction("../User/LoggedIn");
        }
    }
}