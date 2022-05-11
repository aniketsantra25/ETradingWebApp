using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETrading_MVC.Models;
using System.Net.Http;

namespace ETrading_MVC.Controllers
{
    public class BusinessOnwerController : Controller
    {
        int newid;
        // GET: BusinessOnwer

        // To get all the customer details who have bought the shares of the company
        public ActionResult Index(short id)
        {
            List<CustomerVM> customerList = new List<CustomerVM>();
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("https://localhost:44377/api/BusinessOwnertoCustomer/" + id.ToString()).Result;
                if (result.IsSuccessStatusCode)
                {
                    customerList = result.Content.ReadAsAsync<List<CustomerVM>>().Result;
                    return View(customerList);
                }
            }
            return View();
        }

        // GET: BusinessOnwer/Details/5
        // To view the details of the businessOnwer
        public ActionResult Details(int id)
        {
            
            BusinessOwnerVM businessOwner = new BusinessOwnerVM();
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("https://localhost:44377/api/BusinessOwner/" + id.ToString()).Result;
                if (result.IsSuccessStatusCode)
                {
                    businessOwner = result.Content.ReadAsAsync<BusinessOwnerVM>().Result;
                    return View(businessOwner);
                }
            }
            return View();
        }

        // GET: BusinessOnwer/Create
        // To create new BusinessOwner (register)
        public ActionResult Create()
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

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(BusinessOwnerVM bo)
        {
            try
            {
                // TODO: Add insert logic here
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync("https://localhost:44377/api/BusinessOwner", bo).Result;

                if (result.IsSuccessStatusCode)
                {
                    var companyId = result.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Login");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return View();
        }

        // GET: BusinessOnwer/Edit/5
        //To edit details of the businessOwner
        public ActionResult Edit(int id)
        {
            try
            {
                BusinessOwnerVM bo = new BusinessOwnerVM();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/BusinessOwner/" + id.ToString()).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        bo = result.Content.ReadAsAsync<BusinessOwnerVM>().Result;
                        return View(bo);
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return View();
        }

        // POST: BusinessOnwer/Edit/5
        [HttpPost]
        public ActionResult Edit(short id, BusinessOwnerVM bo)
        {
            try
            {
                // TODO: Add update logic here
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44377/api/");

                var result = client.PutAsJsonAsync("BusinessOwner/"+id.ToString(),bo).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToRoute("Default", new { controller =  "BusinessOnwer", action = "Details", id = id });
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        //to login into the businessowner
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(BusinessOwnerLoginVM bo)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync("https://localhost:44377/api/BusinessOwner?email=" + bo.email + "&password=" + bo.password).Result;
            if (result.IsSuccessStatusCode)
            {
                var res = result.Content.ReadAsAsync<Object>().Result;
                if (res.ToString() == "1")
                {
                    Session["id"] = bo.id;
                    Session["username"] = bo.email;
                    return RedirectToAction("Index", new {id = Session["id"]});
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
        //logout
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
