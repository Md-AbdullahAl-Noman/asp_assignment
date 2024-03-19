using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using HungerManagementSystem.DTO;
using HungerManagementSystem.EF;

namespace HungerManagementSystem.Controllers
{
    public class RestaurantController : Controller
    {

        FoodManagementEntities2 db = new FoodManagementEntities2();

        // GET: Restaurant/Index
        public ActionResult Dashboard()
        {

            var collectRequests = db.CollectRequests.ToList();


            var collectRequestDTOs = collectRequests.Select(c => new CollectRequestDTO
            {
                // Maping

                Request_Id = c.Request_Id,
                Restaurant_Id = c.Restaurant_Id,
                Requested_Time = c.Requested_Time,
                Preserve_Time = c.Preserve_Time,
                Status = c.Status,
                EmployeeID = c.EmployeeID
            }).ToList();

            return View(collectRequestDTOs);
        }


        // GET
        public ActionResult SubmitCollectRequest()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SubmitCollectRequest(CollectRequestDTO collectRequestDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {




                    var collectRequest = new CollectRequest
                    {
                        Request_Id = collectRequestDTO.Request_Id,
                        Restaurant_Id = collectRequestDTO.Restaurant_Id,
                        Requested_Time = collectRequestDTO.Requested_Time,
                        Preserve_Time = collectRequestDTO.Preserve_Time,

                        Status = "Pending"
                    };

                    // Add the collect request to the database
                    db.CollectRequests.Add(collectRequest);
                    db.SaveChanges();


                    return RedirectToAction("Dashboard");
                }
                catch (DbUpdateException ex)
                {
                  
                    if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                    {

                        ModelState.AddModelError("", "Please enter a valid restaurant ID.");
                    }
                    else
                    {
                  
                        ViewBag.ErrorMessage = "Please enter a valid restaurant ID.";


                    }


                    return View("SubmitCollectRequest");
                }
            }


            return View("SubmitCollectRequest");
        }

        [HttpGet]
        public ActionResult DeleteCollectRequest(int requestId)
        {
            try
            {
             
                var collectRequest = db.CollectRequests.FirstOrDefault(c => c.Request_Id == requestId);

                if (collectRequest != null)
                {

                    db.CollectRequests.Remove(collectRequest);
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Collect request deleted successfully.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Collect request not found.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to delete collect request.";
               
            }

          
            return RedirectToAction("Dashboard");
        }


        //edit
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var collectRequest = db.CollectRequests.FirstOrDefault(c => c.Request_Id == id);

            if (collectRequest == null)
            {
             
                return HttpNotFound();
            }


            var collectRequestDTO = new CollectRequestDTO
            {
                Request_Id = collectRequest.Request_Id,
                Restaurant_Id = collectRequest.Restaurant_Id,
                Requested_Time = collectRequest.Requested_Time,
                Preserve_Time = collectRequest.Preserve_Time,

            };

      
            return View(collectRequestDTO);
        }

        [HttpPost]

        public ActionResult Edit(CollectRequestDTO collectRequestDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var collectRequest = db.CollectRequests.FirstOrDefault(c => c.Request_Id == collectRequestDTO.Request_Id);

                   

                    collectRequest.Restaurant_Id = collectRequestDTO.Restaurant_Id;
                    collectRequest.Requested_Time = collectRequestDTO.Requested_Time;
                    collectRequest.Preserve_Time = collectRequestDTO.Preserve_Time;
                   
                    collectRequest.Status = "Pending";


                    db.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
                catch (DbUpdateException ex)
                {

                    ViewBag.ErrorMessage = "Failed to update collect request.";
                }
            }

          
            return View(collectRequestDTO);
        }




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(RestaurantDTO r)
        {
            var user = db.Restaurants.Where(model => model.Email == r.Email && model.Password == r.Password).FirstOrDefault();

            if (user != null)
            {

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }
}
