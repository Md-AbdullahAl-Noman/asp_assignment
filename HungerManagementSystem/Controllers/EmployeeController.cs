using System;
using System.Linq;
using System.Web.Mvc;
using HungerManagementSystem.DTO;
using HungerManagementSystem.EF;

namespace HungerManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly FoodManagementEntities2 db = new FoodManagementEntities2();

        // GET: Employee

        public ActionResult Dashboard()
        {
           
            if (Session["EmployeeId"] != null)
            {
                var employee = GetCurrentEmployee();

                var employeeDTO = new EmployeeDTO
                {
                    Employee_Id = employee.Employee_Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone
                };

                return View(employeeDTO);
            }
            else
            {
                
                return RedirectToAction("Login", "Employee"); // Redirect to login 
            }
        }


        public ActionResult Index()
        {
           
            if (Session["EmployeeId"] != null)
            {
                int employeeId = Convert.ToInt32(Session["EmployeeId"]);
                var assignedCollectRequests = db.CollectRequests
                    .Where(c => c.EmployeeID == employeeId && c.Status == "Assigned")
                    .ToList();

                return View(assignedCollectRequests);
            }
            else
            {
                
                return RedirectToAction("Login", "Employee"); 
            }
        }

        private Employee GetCurrentEmployee()
        {
            if (Session["EmployeeId"] != null)
            {
                int employeeId = Convert.ToInt32(Session["EmployeeId"]);
                return db.Employees.FirstOrDefault(e => e.Employee_Id == employeeId);
            }
            else
            {

                throw new InvalidOperationException("User is not logged in.");
            }

           
        }

       

        [HttpPost]
        public ActionResult ConfirmCollectRequest(int collectRequestId)
        {
          
            var collectRequest = db.CollectRequests.FirstOrDefault(c => c.Request_Id == collectRequestId);

            if (collectRequest != null)
            {
               
                collectRequest.Status = "Completed";
                db.SaveChanges(); 
            }

           
            return RedirectToAction("Index");
        }

        // SignUp action
        public ActionResult SignUp()
        {
            var signUpDto = new EmployeeDTO();
            return View(signUpDto);
        }

        [HttpPost]
        public ActionResult SignUp(EmployeeDTO s)
        {
            if (ModelState.IsValid)
            {
                
                // Create a new employee entity and populate it with DTO data
                var newEmployee = new Employee
                {
                    Name = s.Name,
                    Email = s.Email,
                    Password = s.Password, 
                    Phone = s.Phone
                };

                db.Employees.Add(newEmployee);
                db.SaveChanges();

          
                return RedirectToAction("Login");
            }

            
            return View(s);
        }

        // Login action
        public ActionResult Login()
        {
            var loginDto = new EmployeeDTO();
            return View(loginDto);
        }

        [HttpPost]
        public ActionResult Login(EmployeeDTO r)
        {
            
               
                var employee = db.Employees.Where(model => model.Email == r.Email && model.Password == r.Password).FirstOrDefault();
                if (employee != null)
                {
                TempData["SuccessMessage"] = "Login successful. Welcome back, " + employee.Name + "!";
                Session["EmployeeId"] = employee.Employee_Id;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            

            return View(r);
        }
        // Logout action
        public ActionResult Logout()
        {
            
            Session.Clear(); // Clear session data
            Response.Cookies.Clear();

            
            return RedirectToAction("Login", "Employee");
        }

    }
}
