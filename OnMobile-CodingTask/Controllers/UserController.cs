using Newtonsoft.Json;
using OnMobileCodingTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using OnMobileCodingTask.Helper;

namespace OnMobileCodingTask.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDomain userDomain)
        {
            if (ModelState.IsValid)
            {
                UserController userController = new UserController();
                var response = userController.CheckUserStatus(userDomain.MobileNumber);
                if (!string.IsNullOrEmpty(response))
                {
                    TempData["MobileNumber"] = userDomain.MobileNumber;
                    TempData["PackID"] = response;
                    return RedirectToAction("Confirmation");
                    //userController.SubscribeUser(response, userDomain.MobileNumber);
                }
                else
                {
                    return View("AlreadySubscribed");
                }

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [HttpGet]
        public ActionResult Confirmation()
        {
            TempData.Keep();
            return View();

        }

        [HttpPost]
        public ActionResult Confirmation(UserDomain userDomain)
        {
            var response = SubscribeUser(TempData["PackID"].ToString(), TempData["MobileNumber"].ToString());
            if (!response.Equals("ALREADY_ACTIVE"))
            {
                return RedirectToAction("Okay");
            }
            return View("AlreadySubscribed");
        }

        [HttpGet]
        public ActionResult Okay()
        {
            return View("Subscribed");
        }

        [HttpPost]
        public ActionResult Okay(UserDomain userDomain)
        {
            return Redirect("http://www.google.com?status=SUBSCRIBED");
        }

        private string CheckUserStatus(string MobileNumber)
        {
            HttpClient httpClient = new HttpClient();
            string uri1 = @"http://blr-dev.fonestarz.com/integration/onlinetest/api/subscriptions/";
            string finalURI = uri1 + MobileNumber;
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            var result = httpClient.GetAsync(finalURI);
            var content = result.Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ValidationResponse>(content.Result);
            if (response.Status.Length == 0 && response.Pack.Length > 0)
            {
                return response.Pack.FirstOrDefault().Packid.ToString();
            }
            else
            {
                return null;
            }
        }

        private string SubscribeUser(string packid, string mobileNumber)
        {
            HttpClient httpClient = new HttpClient();
            string uri1 = @"http://blr-dev.fonestarz.com/integration/onlinetest/api/subscriptions/add/";
            string finalURI = uri1 + mobileNumber + "/" + packid;
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            var result = httpClient.PostAsync(finalURI, null);
            var content = result.Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SubscriptionResponse>(content.Result);
            return response.Status;
        }

        [HttpGet]
        public ActionResult Cancelled()
        {
            return Redirect("http://www.google.com?status=CANCELLED");
        }
    }
}