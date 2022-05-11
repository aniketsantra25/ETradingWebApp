using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ETradingEF;

namespace ETradingWebApi.Controllers
{
    public class BusinessOwnertoCustomerController : ApiController
    {
        ETradingEntities db = new ETradingEntities();
        // GET: api/BusinessOwnertoCustomer

        // To get all the customer details who have bought the share of the company whose id is passed
        public IHttpActionResult Get(short id)
        {
            try
            {
                var custList = db.GetCustomersAccordingToCompany(id).ToList();         // calling the procedure to get the customer details
                if(custList == null)
                {
                    return NotFound();                  // no such customer exists
                }
                else
                {
                    return Ok(custList);    // returns the list of the customers
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }
    }
}
