using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ETradingEF;

namespace ETradingWebApi.Controllers
{
    public class TransactionController : ApiController
    {
        // GET: api/Transaction
        ETradingEntities db = new ETradingEntities();

        // GET: api/Transaction
        // Transaction -API - Display Share price
        public IHttpActionResult GetSharePrice()
        {
            try
            {
                ObjectResult<proc_CheckSharePrice_Result> b = db.proc_CheckSharePrice();
                return Ok(b);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Transaction/5
        // Transaction -API - Display Transaction details
        public IHttpActionResult GetTransactionDetails(string email)
        {
            try
            {
                var result = db.proc_displayTransactionDetails(email).ToList();

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //GET CompanyDetails
        // Transaction -API - Display Company Details
        public IHttpActionResult GetCompanyDetails(short id)
        {
            try
            {
                var result = db.proc_ViewCompanyDetails(id).SingleOrDefault();

                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Transaction/5
        // Transaction -API -BUYSHARES
        public IHttpActionResult GetBuyShares(short companyId, int shareQuantity, string email)
        {
            try
            {
                ObjectParameter status = new ObjectParameter("status", typeof(Int16));

                db.proc_buyShares(companyId, shareQuantity, email, status);
                var res = status.Value;
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //PUT
        //Transaction - API -SellShares
        public IHttpActionResult GetSellShares(string email, short numberofShares, short companyid)
        {

            try
            {
                ObjectParameter op_price = new ObjectParameter("val", typeof(Int16));
                db.proc_sellShares(numberofShares, companyid, email, op_price);

                return Ok(op_price.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
