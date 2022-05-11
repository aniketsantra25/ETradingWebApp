using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ETrading_MVC.Models;


namespace ETrading_MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        //To get All the BusinessOwners List
        public ActionResult GetBusinessOwnerDetails()
        {
            List<BusinessOwnerVM> businessOwnerList = new List<BusinessOwnerVM>();
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("https://localhost:44377/api/AdminBusinessOwner").Result;
                if (result.IsSuccessStatusCode)
                {
                    businessOwnerList = result.Content.ReadAsAsync<List<BusinessOwnerVM>>().Result;
                    return View(businessOwnerList);
                }
            }
            return View();
        }

        // To get all the customers list
        public ActionResult GetCustomersDetails()
        {
            List<CustomerVM> custList = new List<CustomerVM>();
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("https://localhost:44377/api/AdminCustomer").Result;
                if (result.IsSuccessStatusCode)
                {
                    custList = result.Content.ReadAsAsync<List<CustomerVM>>().Result;
                    return View(custList);
                }
            }
            return View();
        }

        // GET: Admin/Create
        // To create new BusinessOwner
        public ActionResult CreateBusinessOwner()
        {
            List<CategoryVM> catlist = new List<CategoryVM>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync("https://localhost:44377/api/AdminCategory").Result;
            if (result.IsSuccessStatusCode)
            {
                catlist = result.Content.ReadAsAsync<List<CategoryVM>>().Result;
                SelectList sl = new SelectList(catlist, "CategoryId", "CategoryName");
                TempData["category"] = sl;
                TempData.Keep();
            }
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateBusinessOwner(BusinessOwnerVM bo)
        {
            try
            {
                // TODO: Add insert logic here
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync("https://localhost:44377/api/BusinessOwner", bo).Result;

                if (result.IsSuccessStatusCode)
                {
                    var companyId = result.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("GetBusinessOwnerDetails");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return View();
        }

        // To create new customer
        public ActionResult createCustomers()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult createCustomers(CustomerVM cust)
        {
            try
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync("https://localhost:44377/api/AdminCustomer", cust).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetCustomersDetails");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return View();
        }
        // GET: Admin/Delete/5
        public ActionResult DeleteCustomer(short id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var result = client.DeleteAsync("https://localhost:44377/api/AdminCustomer/" + id.ToString()).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetCustomersDetails");
                }
                else
                {
                    TempData["Message"] = "Customer can't be deleted";
                    TempData.Keep();
                    return RedirectToAction("GetCustomersDetails");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("GetCustomersDetails");
            }
            return View("GetCustomersDetails");
        }

        //Admin Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLoginVM ad)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("https://localhost:44377/api/Admin?email=" + ad.email + "&password=" + ad.password).Result;
            if (result.IsSuccessStatusCode)
            {
                var res = result.Content.ReadAsAsync<Object>().Result;
                if (res.ToString() == "1")
                {
                    Session["username"] = ad.email;
                    return RedirectToAction("GetCustomersDetails");
                }
                else
                {
                    ViewBag.Message = "Invalid Username/Password";
                    Session["auth"] = "Authorization Failed";
                    return View();
                }

            }
            return View();

        }

        // To logout
        public ActionResult Logout()
        {
            if (Session["username"] != null)
            {
                Session["username"] = null;
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}
