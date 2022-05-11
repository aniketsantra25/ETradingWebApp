using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Core.Objects;
using ETradingEF;

namespace ETradingWebApi.Controllers
{
    public class AdminController : ApiController
    {
        ETradingEntities db = new ETradingEntities();

        // GET: api/Admin/5
        // To login into admin view
        public IHttpActionResult GetLogin(string email, string password)
        {
            try
            {
                ObjectParameter login = new ObjectParameter("val", typeof(Int16));
                db.proc_AdminLogin(email, password, login);
                return Ok(login.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        
    }
}
