using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ETrading_MVC.Models;

namespace ETrading_MVC.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        //CustomerTransaction- MVC -Display Shareprice
        public ActionResult Index()
        {
            try
            {
                List<SharePriceVM> spList = new List<SharePriceVM>();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/Transaction").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        spList = result.Content.ReadAsAsync<List<SharePriceVM>>().Result;
                        return View(spList);
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

        // GET: Transaction/Details/5
        //CustomerTransaction - MVC -Display Transaction
        public ActionResult DetailTransaction(string email)
        {
            try
            {
                List<TransactionVM> tranList = new List<TransactionVM>();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/Transaction?email=" + email).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        tranList = result.Content.ReadAsAsync<List<TransactionVM>>().Result;
                        return View(tranList);
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
        //Get company list
        //CustomerTransaction - MVC - Display All Company
        public ActionResult GetCompanyList()
        {
            try
            {
                List<BusinessOwnerVM> complist = new List<BusinessOwnerVM>();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/AdminBusinessOwner").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        complist = result.Content.ReadAsAsync<List<BusinessOwnerVM>>().Result;

                        return View(complist);
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
        //GET Company details
        //CustomerTransaction - MVC -Display CompanyDetails
        public ActionResult DisplayCompanyDetails(short id)
        {
            try
            {
                BusinessOwnerVM com = new BusinessOwnerVM();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/Transaction/" + id.ToString()).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        com = result.Content.ReadAsAsync<BusinessOwnerVM>().Result;
                        return View(com);
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

        //BuyShares 
        //CustomerTransaction - MVC - Buyshares
        [HttpGet]
        public ActionResult BuyShares(string email)
        {
            try
            {
                List<BuySharesVM> buyshareList = new List<BuySharesVM>();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/AdminBusinessOwner").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        buyshareList = result.Content.ReadAsAsync<List<BuySharesVM>>().Result;
                        SelectList sl = new SelectList(buyshareList, "CompanyId", "CompanyName");

                        TempData["company"] = sl;
                        TempData.Keep();
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

        //BuyShares
        //CustomerTransaction -MVC-Buyshares
        [HttpPost]
        public ActionResult BuyShares(string email, BuySharesVM bs)
        {
            try
            {
                TempData.Keep();
                // TODO: Add update logic here
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/transaction?companyId=" + bs.companyId.ToString() + "&shareQuantity=" + bs.shareQuantity.ToString() + "&email=" + email).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var status = result.Content.ReadAsAsync<Object>().Result;
                        ModelState.Clear();

                        //ViewBag.msg = status;                        
                        if (status.ToString().Equals("0"))
                        {
                            ViewBag.msg = "Unable to Buy ,Not enough share";
                        }
                        else if (status.ToString().Equals("1"))
                        {
                            ViewBag.msg = "Unable to Buy,insufficient balance";
                        }
                        else if (status.ToString().Equals("2"))
                        {
                            ViewBag.msg = "Share bought successfully";
                        }
                        else if (status.ToString().Equals("3"))
                        {
                            ViewBag.msg = "No such company";
                        }
                        return View();
                    }
                }
                return RedirectToAction("detailtransaction", new { email = email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        //CustomerTransaction - MVC -Sellshares
        [HttpGet]
        public ActionResult SellShares(string email)
        {
            try
            {
                List<SellSharesVM> buyshareList = new List<SellSharesVM>();
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/AdminBusinessOwner").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        buyshareList = result.Content.ReadAsAsync<List<SellSharesVM>>().Result;
                        SelectList sl = new SelectList(buyshareList, "companyid", "CompanyName");
                        TempData["company"] = sl;
                        TempData.Keep();
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

        //SellShares
        //CustomerTransaction - MVC -Sellshares
        [HttpPost]
        public ActionResult SellShares(string email, SellSharesVM ss)
        {
            try
            {
                TempData.Keep();
                // TODO: Add update logic here
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44377/api/transaction?email=" + email + "&numberofShares=" + ss.numberofShares + "&companyid=" + ss.companyid).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var status = result.Content.ReadAsAsync<short>().Result;
                        ModelState.Clear();

                        //ViewBag.res = status;
                        if (status.ToString().Equals("0"))
                        {
                            ViewBag.msg = "Shares Sold Successfully";
                        }
                        else if (status.ToString().Equals("1"))
                        {
                            ViewBag.msg = "Unable to sell,Not enough shares";
                        }
                        else if (status.ToString().Equals("2"))
                        {
                            ViewBag.msg = "Unable to sell,No shares of this company ";
                        }
                        else if (status.ToString().Equals("3"))
                        {
                            ViewBag.msg = "No such company";
                        }
                        return View();
                    }
                }
                return RedirectToAction("detailtransaction", new { email = email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

    }
}
