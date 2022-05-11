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
    public class BusinessOwnerController : ApiController
    {
        ETradingEntities db = new ETradingEntities();
        // GET: api/Default/5

        //To get the details of a BusinessOwner
        public IHttpActionResult Get(short id)
        {
            try
            {
                var businessOwner = db.proc_Search_Owner(id).SingleOrDefault();   // Search procedure is called to check whether the businessowner exists or not
                if (businessOwner == null)
                {
                    return NotFound();            // returns not found if businessowner doesn't exists
                }
                else
                {
                    return Ok(businessOwner);     // returns the data of the businessOwner
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        public IHttpActionResult GetLogin(string email, string password)
        {
            try
            {
                ObjectParameter login = new ObjectParameter("val", typeof(Int16));
                db.proc_BusinessOwnerLogin(email, password, login);
                return Ok(login.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }


        // to create a new businessowner
        public IHttpActionResult Post(BusinessOwner businessOwner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ObjectParameter companyId = new ObjectParameter("companyId", typeof(Int16));
                db.proc_Insert_Owner(companyId, businessOwner.OwnerEmail, businessOwner.CompanyPasscode, businessOwner.CompanyName, businessOwner.OwnerName,            // calling the insert pocedure to add new businessowner
                    businessOwner.DateOfIncorporation, businessOwner.PhoneNumber, businessOwner.CategoryId, businessOwner.CompanyAddress, businessOwner.SharePrice,
                    businessOwner.NoOfShares);
                //return CreatedAtRoute("DefaultApi", new { id = businessOwner.CompanyId }, businessOwner);
                return Ok(companyId.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Default/5

        // to update a businessOnwers details
        public IHttpActionResult Put(short id, BusinessOwner b)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                db.proc_Update_Owner(id, b.CompanyPasscode, b.OwnerName, b.OwnerEmail, b.DateOfIncorporation, b.PhoneNumber, b.CompanyAddress,              //update procedure is called to update the fields
                    b.SharePrice, b.NoOfShares);
                
                return CreatedAtRoute("DefaultApi", new { id = b.CompanyId }, b);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }
    }
}
