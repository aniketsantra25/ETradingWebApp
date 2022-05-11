using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ETradingEF;

namespace ETradingWebApi.Controllers
{
    public class AdminBusinessOwnerController : ApiController
    {
        ETradingEntities db = new ETradingEntities();
        // GET: api/AdminBusinessOwner
        public IHttpActionResult GetBusinessOwners()
        {
            try
            {
               
                var boList = db.BusinessOwners.Select(e => new
                {
                    e.CompanyId,
                    e.CompanyName,
                    e.OwnerName,
                    e.OwnerEmail,
                    e.DateOfIncorporation,
                    e.CategoryDetail.CategoryName,
                    e.PhoneNumber,
                    e.CompanyAddress,
                    e.SharePrice,
                    e.NoOfShares
                }).ToList();
                return Ok(boList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        // POST: api/AdminBusinessOwner
        public IHttpActionResult Post(BusinessOwner b)
        {
            try
            {
                db.BusinessOwners.Add(b);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = b.CompanyId }, b);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
    }
}
