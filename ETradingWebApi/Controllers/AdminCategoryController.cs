using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ETradingEF;

namespace ETradingWebApi.Controllers
{
    public class AdminCategoryController : ApiController
    {

        // GET: api/AdminCategory
        // To get all the categories
        public IHttpActionResult GetCategories()
        {
            try
            {
                ETradingEntities db = new ETradingEntities();
                var ctgList = db.CategoryDetails.Select(e => new {
                    e.CategoryId,
                    e.CategoryName
                }).ToList();
                return Ok(ctgList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
