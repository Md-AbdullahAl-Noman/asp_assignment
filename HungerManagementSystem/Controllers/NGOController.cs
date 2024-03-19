using HungerManagementSystem.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungerManagementSystem.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index()
        {
            return View();
        }

        private FoodManagementEntities2 db = new FoodManagementEntities2();

        // GET: NGO Dashboard
        public ActionResult Dashboard()
        {
            var collectRequests = db.CollectRequests.ToList();
            return View(collectRequests);
        }

        public ActionResult ReviewCollectRequests()
        {
           
            var pendingCollectRequests = db.CollectRequests.Where(c => c.Status == "Pending").ToList();
            return View(pendingCollectRequests);
        }

        [HttpPost]
        public ActionResult ApproveCollectRequest(int collectRequestId)
        {
            // Retrieve the collect request from the database
            var collectRequest = db.CollectRequests.FirstOrDefault(c => c.Request_Id == collectRequestId);

            if (collectRequest != null)
            {
                // Update  the collect request to "Approved"
                collectRequest.Status = "Approved";
                db.SaveChanges(); // Save changes to the database
            }

            return RedirectToAction("ReviewCollectRequests");
        }


        //assign

        public ActionResult AssignCollectRequests()
        {
            
            var approvedCollectRequests = db.CollectRequests.Where(c => c.Status == "Approved" && c.EmployeeID == null).ToList();
            return View(approvedCollectRequests);
        }

        [HttpPost]
        public ActionResult AssignCollectRequest(int collectRequestId, int? employeeId)
        {
            try
            {
                if (employeeId == null)
                {
                    throw new ArgumentException("Employee ID cannot be null.");
                }

                var collectRequest = db.CollectRequests.FirstOrDefault(c => c.Request_Id == collectRequestId);

                if (collectRequest != null)
                {
                    collectRequest.EmployeeID = employeeId.Value;
                    // Update the status to "Assigned"
                    collectRequest.Status = "Assigned";
                    db.SaveChanges();
                   
                }

                return RedirectToAction("Dashboard");
            }
            catch (ArgumentException ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
                var approvedCollectRequests = db.CollectRequests
                    .Where(c => c.Status == "Approved" && c.EmployeeID == null)
                    .ToList();
                return View("Dashboard", approvedCollectRequests);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    ViewBag.ErrorMessage = "The selected employee does not exist. Please select a valid employee or register.";
                    var approvedCollectRequests = db.CollectRequests
                    .Where(c => c.Status == "Approved" && c.EmployeeID == null)
                    .ToList();
                    return View("Dashboard", approvedCollectRequests);
                }
                else
                {
                    ViewBag.ErrorMessage = "Error:The selected employee does not exist. Please select a valid employee or register.";
                    var approvedCollectRequests = db.CollectRequests
                    .Where(c => c.Status == "Approved" && c.EmployeeID == null)
                    .ToList();
                    return View("Dashboard", approvedCollectRequests);
                }

                
                
            }
        }


    }
}