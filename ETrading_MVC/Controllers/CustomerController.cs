using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ETrading_MVC.Models;

namespace ETrading_MVC.Controllers
{
    public class CustomerController : Controller
    {
        //GET : Login
        //Customer - MVC -Login - httpget
        public ActionResult LoginCustomer()
        {
            return View();
        }

        //LOGIN-Customer -MVC -HTTPPOST
        [HttpPost]
        public ActionResult LoginCustomer(LoginVM lg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var result = client.GetAsync("https://localhost:44377/api/Customer?email=" + lg.email + "&password=" + lg.password).Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var res = result.Content.ReadAsAsync<Object>().Result;

                            if (res.ToString().Equals("1"))
                            {
                                Session["UserEmail"] = lg.email;

                                return RedirectToAction("details", new { email = Session["UserEmail"] });
                            }
                            else if (res.ToString().Equals("0"))
                            {
                                ViewBag.msg = "Invalid Username/Password";
                            }
                            return View();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return View();
        }

        // GET: Customer/Details/5
        //Customer -MVC-View Profile
        public ActionResult Details(string email)
        {
            try
            {
                CustomerVM custList = new CustomerVM();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/Customer?email=" + email).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        custList = result.Content.ReadAsAsync<CustomerVM>().Result;
                        return View(custList);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return View();
        }

        //GET Account Balance
        // Customer - MVC -Display balance
        public ActionResult GetBalance(string mail)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/Customer?mail=" + mail).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var balance = result.Content.ReadAsAsync<Object>().Result;
                        ViewBag.res = "Account Balance :  " + balance;
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return View();
        }

        // GET: Customer/Create
        // Customer - MVC - Register
        public ActionResult CreateCustomer()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult CreateCustomer(CustomerVM cust)
        {
            try
            {
                // TODO: Add insert logic here
                using (HttpClient client = new HttpClient())
                {
                    var result = client.PostAsJsonAsync("https://localhost:44377/api/Customer", cust).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var custid = result.Content.ReadAsAsync<Object>().Result;
                        ModelState.Clear();
                        ViewData["custid"] = "CUSTOMER ID: " + custid;
                        return RedirectToAction("LoginCustomer");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Customer/Edit/5
        // Customer - Mvc -Edit profile
        public ActionResult Edit(string email)
        {
            try
            {
                CustomerVM custList = new CustomerVM();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/Customer?email=" + email).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        custList = result.Content.ReadAsAsync<CustomerVM>().Result;
                        return View(custList);
                    }
                }
                return View("Details");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // POST: Customer/Edit/5
        // Customer - MVC - Edit Profile
        [HttpPost]
        public ActionResult Edit(string email, CustomerVM cust)
        {
            try
            {
                // TODO: Add update logic here
                using (HttpClient client = new HttpClient())
                {
                    var result = client.PutAsJsonAsync("https://localhost:44377/api/Customer?email=" + email, cust).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return View("details", cust);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        //Customer - MVC -Logout
        public ActionResult Logout()
        {
            if (Session["username"] != null)
            {
                Session["username"] = null;
                return RedirectToAction("LoginCustomer");
            }
            else
            {
                return RedirectToAction("LoginCustomer");
            }
        }
    }
}
