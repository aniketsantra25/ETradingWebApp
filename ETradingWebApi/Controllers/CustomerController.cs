using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Core.Objects;
using ETradingEF;
using System.Web.Http.Description;

namespace ETradingWebApi.Controllers
{
    [Route("api/customer/{id?}")]
    public class CustomerController : ApiController
    {
        ETradingEntities db = new ETradingEntities();

        //GET CustomerLogin/API
        public IHttpActionResult GetLogin(string email, string password)
        {
            try
            {
                ObjectParameter status = new ObjectParameter("loginStatus", typeof(Int16));
                db.proc_Customer_Login(email, password, status);
                return Ok(status.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Customer/5
        //View Profile
        public IHttpActionResult GetViewProfile(string email)
        {
            try
            {
                var cd = db.proc_ViewDetails(email).SingleOrDefault();
                if (cd == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(cd);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //GET Customer- API -Account balance
        public IHttpActionResult GetBalance(string mail)
        {
            try
            {
                ObjectResult<decimal?> bal = db.proc_Checkbalance_Customer(mail);

                if (bal == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(bal);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        // POST: api/Customer
        //Customer - API- Register
        [ResponseType(typeof(CustomerDetail))]
        public IHttpActionResult PostCreateCustomer([FromBody] CustomerDetail cust)
        {
            try
            {
                ObjectParameter custnumber = new ObjectParameter("custId", typeof(Int16));
                db.proc_customerInsert(custnumber, cust.CustomerName, cust.CustomerEmail, cust.Password, cust.DOB, cust.PhoneNumber, cust.AccountBalance, cust.Address);
                return Ok(custnumber.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Customer/5
        // Customer - API - EditProfile 
        public IHttpActionResult PutEditProfile(string email, [FromBody] CustomerDetail cust)
        {
            try
            {
                int rowUpdated = db.proc_UpdateCustomerDetails(email, cust.CustomerName, cust.Password, cust.CustomerEmail, cust.DOB, cust.PhoneNumber, cust.AccountBalance, cust.Address);
                if (rowUpdated == 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
