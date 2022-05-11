using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ETradingEF;
using ETradingWebApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace ETrading_WebApi_UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAdminCustomerTest()
        {
            AdminCustomerController controller = new AdminCustomerController();
            IHttpActionResult actionResult = controller.Get();
            Assert.AreEqual((OkNegotiatedContentResult<List<proc_ViewAllCustomers_Result>>)actionResult, actionResult);
        }
        [TestMethod]
        public void GetAdminBusinessOwnerTest()
        {
            BusinessOwnerController controller = new BusinessOwnerController();
            IHttpActionResult actionResult = controller.Get(2);
            Assert.AreEqual((OkNegotiatedContentResult<proc_Search_Owner_Result>)actionResult, actionResult);
        }
        [TestMethod]
        public void GetAdminCustomerNotFoundTest()
        {
            AdminCustomerController controller = new AdminCustomerController();
            IHttpActionResult actionResult = controller.Get("2");
            Assert.AreEqual((NotFoundResult)actionResult, actionResult);
        }

        [TestMethod]
        public void DeleteCustomers()
        {
            AdminCustomerController controller  = new AdminCustomerController();
            IHttpActionResult actionResult = controller.DeleteCustomer(114);
            Assert.AreEqual((OkResult)actionResult, actionResult);
        }

        [TestMethod]
        public void CreateCustomers()
        {
            AdminCustomerController controller = new AdminCustomerController();
            IHttpActionResult actionResult = controller.Post(new CustomerDetail { CustomerName = "Alok", CustomerEmail = "a@gmail.com", Password = "aaa123@", DOB = DateTime.Parse("2000-01-21"), PhoneNumber = "3432432421", AccountBalance = 12312, Address = "Kolkata" });
            Assert.AreEqual((CreatedAtRouteNegotiatedContentResult<CustomerDetail>)actionResult, actionResult);
        }
     
    }
}
