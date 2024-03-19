using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HungerManagementSystem.DTO
{
    public class FoodItemDTO
    {
        public int Item_Id { get; set; }
        public int Collect_Request_Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public System.DateTime ExpiryDate { get; set; }

    }
}