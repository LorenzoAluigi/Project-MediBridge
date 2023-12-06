using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MediBridge.Models.Class
{
    public class TplanDTO
    {
       public int medicalConditionId {  get; set; }
       public string planDescription { get; set; }
       public DateTime startDate {  get; set; }
       public DateTime expiryDate{get; set; }



    }
}