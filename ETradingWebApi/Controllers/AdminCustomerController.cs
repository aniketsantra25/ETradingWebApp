using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ETradingEF;

namespace ETradingWebApi.Controllers
{
    public class AdminCustomerController : ApiController
    {
        ETradingEntities db = new ETradingEntities();
        // GET: api/AdminCustomer
        // To get all customer details
        public IHttpActionResult Get()
        {
            try
            {
                var custList = db.proc_ViewAllCustomers().ToList();
                return Ok(custList);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        // GET: api/AdminCustomer/5
       // [Route("api/AdminCustomer/{email}")]
        //[HttpGet]
        // To get particular customer details
        public IHttpActionResult Get(string id)
        {
            try
            {
                var customer = db.proc_ViewDetails(id).SingleOrDefault();
                if (customer == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(customer);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/AdminCustomer
        //To add new customer
        public IHttpActionResult Post(CustomerDetail c)
        {
            try
            {
                db.CustomerDetails.Add(c);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = c.CustomerId }, c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/AdminCustomer/5
        //To delete a acustomer
        public IHttpActionResult DeleteCustomer(int id)
        {
            try
            {
                var cust = db.CustomerDetails.FirstOrDefault(e => e.CustomerId == id);
                if (cust != null)
                {
                    db.CustomerDetails.Remove(cust);
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Customer with id " + id.ToString() + " not found");
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, "Customer with id " + id.ToString() + " not found");
            }
        }
    }
}
