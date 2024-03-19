using System;
using System.ComponentModel.DataAnnotations;

namespace HungerManagementSystem.DTO
{
    public class CollectRequestDTO
    {
        [Display(Name = "Request ID")]
        public int Request_Id { get; set; }

        [Display(Name = "Restaurant ID")]
        [Required(ErrorMessage = "Restaurant ID is required")]
        public int Restaurant_Id { get; set; }

        [Display(Name = "Requested Time")]
        [Required(ErrorMessage = "Requested Time is required")]
        public DateTime Requested_Time { get; set; }

        [Display(Name = "Max Preserve Time")]
        [Required(ErrorMessage = "Preserve Time is required")]
        public DateTime Preserve_Time { get; set; }

        [Display(Name = "Status")]
        
        public string Status { get; set; }
      
        public Nullable<int> EmployeeID { get; set; }
    }
}
