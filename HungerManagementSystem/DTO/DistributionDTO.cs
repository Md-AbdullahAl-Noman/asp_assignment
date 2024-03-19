using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HungerManagementSystem.DTO
{
    public class DistributionDTO
    {
        public int Distribute_Id { get; set; }
        public int Collect_Id { get; set; }
        public int Emp_Id { get; set; }
        public System.DateTime Distribution_Time { get; set; }

        
    }
}